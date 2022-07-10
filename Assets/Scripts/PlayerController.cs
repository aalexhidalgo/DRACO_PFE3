using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Controladores de DRACO
    public float Speed = 8f;
    public float UpSpeed = 20f;
    public float FlySpeed = 3f;

    public float HorizontalInput;

    private float YRotationLimit = 90;
    //private float SkyLimit = 16.5f;

    private Rigidbody DracoRigidbody;
    private Vector3 NewGravity = new Vector3 (0f, -29.4f, 0f);

    public GameObject FuegoPrefab;

    private SpriteRenderer DracoSprite;
    public Sprite[] DracoSpritesArray;
    private bool IsFlying;

    //Contadores Props
    public float CurrentLive = 3;
    public int Shield = 0;
    public int MaxShieldValue = 1;
    private int Multiply = 2;
    public int MoneyCounter = 0;
    private float MaxFlyTime = 0.5f; //Max_S
    private float CurrentTime;  //Timepassed(S)
    private float AntiTime;

    //Booleanas de condiciones
    public bool IsOnTheGround;
    public bool CanFly;
    public bool ShootFire;

    //Comunicación con scripts
    private PropLogic PropLogicScript;
    private EnemyLogic EnemyLogicScript;
    private GameManager GameManagerScript;

    public GameObject GameOverPanel;
    public bool GameOver;

    //AudioSources para acceder a sonidos

    public AudioSource GameManagerAudioSource;

    //AudioClips
    public AudioClip Jumping; //funciona
    public AudioClip GameOverSound; //funciona
    public AudioClip FireSound; //funciona
    public AudioClip CoinSound; //funciona
    public AudioClip RockExplotion; //funciona
    public AudioClip RecogerItem; //funciona

    // Start is called before the first frame update
    void Start()
    {
        MaxShieldValue = DataPersistance.ShieldValue + 1;
        MaxFlyTime = DataPersistance.FlyValue;
        MoneyCounter = PlayerPrefs.GetInt("Money_Counter");
        GameOver = false;

        DracoRigidbody = GetComponent<Rigidbody>();
        Physics.gravity = NewGravity;

        DracoSprite = GetComponent<SpriteRenderer>();

        PropLogicScript = FindObjectOfType<PropLogic>();
        EnemyLogicScript = FindObjectOfType<EnemyLogic>();
        GameManagerScript = FindObjectOfType<GameManager>();

        FlySpeed = FlySpeed + Physics.gravity.magnitude;
        UpdateShield();
        UpdateShieldImage();

        GameManagerAudioSource = GameObject.Find("GameManager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameOver == false)
        {
           if (GameManagerScript.pause == false)
           {
                StartCoroutine(DracoWalking());
                StartCoroutine(DracoFlying());

                //Controladores principales de DRACO

                //Movimiento horizontal
                //TECLADO: RightArrow, LeftArrow o bien A D.
                //GAMEPAD: Joystick Axis X (izquierdo), 3rd Axis Joystick (derecho)

                HorizontalInput = Input.GetAxis("Horizontal");
                DracoRigidbody.AddForce(Vector3.forward * Speed * HorizontalInput);

                //Invertimos su escala con tal de que si avanzamos hacia la izquierda nuestro personaje no va de espaldas hacia esa dirección
                if (HorizontalInput < 0)
                {
                    transform.rotation = Quaternion.Euler(0, YRotationLimit, 0);
                }
                if (HorizontalInput > 0)
                {

                    transform.rotation = Quaternion.Euler(0, -YRotationLimit, 0);
                }

                //Salto
                //TECLADO: Spacebar.
                //GAMEPAD: Joystick button 1 (X).

                if (Input.GetButtonDown("UpMove") && IsOnTheGround) //X, Axis
                {
                    DracoRigidbody.AddForce(Vector3.up * UpSpeed, ForceMode.Impulse);
                    //Evitamos doble salto
                    GameManagerAudioSource.PlayOneShot(Jumping);
                    DracoSprite.sprite = DracoSpritesArray[3];
                    IsOnTheGround = false;
                }

                //Vuelo
                //TECLADO: Q.
                //GAMEPAD: Joystick button 7.
                if (Input.GetButton("FlyMove") && GameManagerScript.FlybarCounter > 0)
                {
                    DracoRigidbody.velocity = Vector3.up * FlySpeed + DracoRigidbody.velocity.x * Vector3.right;
                    IsOnTheGround = false;
                    IsFlying = true;

                    //Tiempo de vuelo
                    CurrentTime += Time.deltaTime;
                    AntiTime = MaxFlyTime - CurrentTime;
                    GameManagerScript.FlybarCounter = AntiTime / MaxFlyTime;
                    GameManagerScript.Flybar.fillAmount = GameManagerScript.FlybarCounter;
                }

                //Fuego
                //TECLADO: E.
                //GAMEPAD: Joystick button 2.
                if (Input.GetButtonDown("Fire"))
                {
                    StartCoroutine(FireCooldown());
                }


                if (IsOnTheGround == true && HorizontalInput == 0) //al estar en el suelo y no estar en movimiento cambiamos el sprite a estado neutral o idle
                {
                    DracoSprite.sprite = DracoSpritesArray[1];
                }

                if(IsOnTheGround == true)
                {
                    IsFlying = false;
                }

            }     
            
        }
        
    }

    private IEnumerator DracoWalking() //cambio de sprites al caminar
    {
        while(IsOnTheGround == true && HorizontalInput != 0)
        {
            DracoSprite.sprite = DracoSpritesArray[0];
            yield return new WaitForSeconds(0.2f);
            DracoSprite.sprite = DracoSpritesArray[1];
            yield return new WaitForSeconds(0.2f);
            DracoSprite.sprite = DracoSpritesArray[2];
        }
    }

    private IEnumerator DracoFlying() //cambio de sprites al volar
    {
        while (IsFlying == true)
        {
            DracoSprite.sprite = DracoSpritesArray[4];
            yield return new WaitForSeconds(0.2f);
            DracoSprite.sprite = DracoSpritesArray[5];
            yield return new WaitForSeconds(0.2f);
            DracoSprite.sprite = DracoSpritesArray[6];
        }
    }

    public void OnCollisionEnter(Collision otherCollider)
    {
        //Si colisiona contra el suelo el jugador puede volver a saltar
        if (otherCollider.gameObject.CompareTag("Ground"))
        {
            IsOnTheGround = true;
        }

        /*if (otherCollider.gameObject.CompareTag("Wall"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }*/

        if(otherCollider.gameObject.CompareTag("Lava") || otherCollider.gameObject.CompareTag("Water"))
        {
            CurrentLive = 0;
            UpdateLife();
            GameOver = true;
            GameOverPanel.SetActive(true);
        }

        //Si jugador pierde vida si colisiona contra un enemigo
        if (otherCollider.gameObject.CompareTag("Enemy") && Shield == 0)
        {
            CurrentLive -= 0.5f;
            Debug.Log("Cuidao que te pinsho dragón de mierda");
            //AddForce rebote, hay que calcular a lo 100tifiko otro día
            if (CurrentLive <= 0)
            {
                CurrentLive = 0;
                Debug.Log("Sa matao Paco");
                GameOver = true;
                GameOverPanel.SetActive(true);
            }

            UpdateLife();
        }

        else if (otherCollider.gameObject.CompareTag("Enemy") && Shield == 1)
        {
            MaxShieldValue -= 1;
            if(MaxShieldValue <= 0)
            {
                Shield = 0;
                UpdateShield();
                Debug.Log("Te quedas sin escudo crack");
            }
            else
            {
                UpdateShieldImage();
            }
        }

        if(GameOver == true) //si muero, paro la música y pongo el sonido de muerte
        {
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Pause();
            GameManagerAudioSource.PlayOneShot(GameOverSound);
        }

        
    }

    public void OnTriggerEnter(Collider otherTrigger)
    {
        //Actualizamos el número de monedas recogidas
        if (otherTrigger.gameObject.CompareTag("Money"))
        {
            GameManagerAudioSource.PlayOneShot(CoinSound);  
            Destroy(otherTrigger.gameObject);
            MoneyCounter += 5;
            Debug.Log($"Tienes {MoneyCounter} monedas, crack");
            UpdateMoney();
        }

        //Actualizamos la vida del jugador
        if (otherTrigger.gameObject.CompareTag("Live"))
        {
            GameManagerAudioSource.PlayOneShot(RecogerItem);
            CurrentLive++;

            if (CurrentLive >= 3)
            {
                CurrentLive = 3f;
                MoneyCounter += 10;
                UpdateMoney();
                Destroy(otherTrigger.gameObject);
            }
            else
            {
                
                Debug.Log($"Tienes {CurrentLive} vidas, crack");
            }

            UpdateLife();

        }

        if (otherTrigger.gameObject.CompareTag("Spike") && Shield == 0)
        {
            CurrentLive = 0;

            Debug.Log("Chanelazo");
            GameOver = true;
            GameOverPanel.SetActive(true);

            UpdateLife();
        }

        else if (otherTrigger.gameObject.CompareTag("Spike") && Shield == 1)
        {
            Shield = 0;
            UpdateShield();
        }

        //Daño de los enemigos al jugador (proyectil)
        if (otherTrigger.gameObject.CompareTag("Bullet") && Shield == 1)
        {
            MaxShieldValue -= 1;
            if (MaxShieldValue <= 0)
            {
                Shield = 0;
                UpdateShield();
                Debug.Log("Te quedas sin escudo crack");
            }
            else
            {
                UpdateShieldImage();
            }
        }

        if (otherTrigger.gameObject.CompareTag("Bullet") && Shield == 0)
        {
            Destroy(otherTrigger.gameObject);
            CurrentLive -= 0.5f;

            if (CurrentLive <= 0)
            {
                CurrentLive = 0;
                Debug.Log("Quack, quack, quack...");
                GameOver = true;
                GameOverPanel.SetActive(true);
                UpdateLife();
            }

            UpdateLife();

        }


        //Si recogemos una nube, permite volar al jugador
        if (otherTrigger.gameObject.CompareTag("Cloud"))
        {
            GameManagerAudioSource.PlayOneShot(RecogerItem);
            GameManagerScript.Flybar.fillAmount = 1;
            GameManagerScript.FlybarCounter = 1;
            Destroy(otherTrigger.gameObject);
        }

        if(otherTrigger.gameObject.CompareTag("Shield"))
        {
            GameManagerAudioSource.PlayOneShot(RecogerItem);
            Shield = 1;
            UpdateShield();
            Destroy(otherTrigger.gameObject);
        }

        
    }

    //Actualizamos la imagen según la vida del jugador
    public void UpdateLife()
    {
       float CurrentImage = CurrentLive * Multiply;
       GameManagerScript.LifeImage.sprite = GameManagerScript.LifeSprites[(int)CurrentImage];
    }

    public void UpdateShield()
    {
        if(Shield == 1)
        {
            GameManagerScript.ShieldImage.SetActive(true);
        }
        else
        {
            GameManagerScript.ShieldImage.SetActive(false);
        }
    }

    public void UpdateShieldImage()
    {
        if(MaxShieldValue == 1)
        {
            GameManagerScript.ShieldImage.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            GameManagerScript.ShieldState.sprite = GameManagerScript.ShieldSprites[MaxShieldValue - 2];
        }       
    }

    //Actualizamos el contador de monedas
    public void UpdateMoney()
    {
        GameManagerScript.MoneyText.text = $"{MoneyCounter}";
    }

    //Cooldown del fuego, el jugador solo podrá disprar cada 0.5 segundos
    public IEnumerator FireCooldown()
    {
        float FireTimer = 0.5f;
        if (ShootFire == true)
        {
            Instantiate(FuegoPrefab, transform.GetChild(0).transform.position, transform.rotation);
            GameManagerAudioSource.PlayOneShot(FireSound);
            ShootFire = false;
        }
        yield return new WaitForSeconds(FireTimer);
        ShootFire = true;
    }


}
