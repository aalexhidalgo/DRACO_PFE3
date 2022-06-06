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
    private float SkyLimit = 16.5f;

    private Rigidbody DracoRigidbody;
    private Vector3 NewGravity = new Vector3 (0f, -29.4f, 0f);

    public GameObject FuegoPrefab;

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

    //Comunicaci�n con scripts
    private PropLogic PropLogicScript;
    private EnemyLogic EnemyLogicScript;
    private GameManager GameManagerScript;

    public GameObject GameOverPanel;
    public bool GameOver;

    //AudioSources para acceder a sonidos

    public AudioSource GameManagerAudioSource;

    //AudioClips
    public AudioClip Walking; //Da problemas, de momento no funciona bien
    public AudioClip Jumping; //funciona
    public AudioClip GameOverSound; //funciona
    public AudioClip FireSound; //funciona
    public AudioClip CoinSound; //funciona
    public AudioClip RockExplotion; //funciona
    public AudioClip RecogerItem; //funciona

    // Start is called before the first frame update
    void Start()
    {
        MaxShieldValue = DataPersistance.DracoState.ShieldValue + 1;
        MaxFlyTime = DataPersistance.DracoState.FlyValue;
        MoneyCounter = PlayerPrefs.GetInt("Money_Counter");
        GameOver = false;

        DracoRigidbody = GetComponent<Rigidbody>();
        Physics.gravity = NewGravity;

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
                //Controladores principales de DRACO

                //Movimiento frontal de DRACO, derecha, izquierda o bien A D
                HorizontalInput = Input.GetAxis("Horizontal");
                DracoRigidbody.AddForce(Vector3.forward * Speed * HorizontalInput);

                //Invertimos su escala con tal de que si avanzamos hacia la izquierda nuestro personaje no va de espaldas hacia esa direcci�n
                if (HorizontalInput < 0)
                {
                    transform.rotation = Quaternion.Euler(0, YRotationLimit, 0);
                }
                if (HorizontalInput > 0)
                {

                    transform.rotation = Quaternion.Euler(0, -YRotationLimit, 0);
                }

                if (HorizontalInput != 0 && IsOnTheGround == true) //Si me muevo y estoy en el suelo se reproduce el sonido
                {
                    //GameManagerAudioSource.PlayOneShot(Walking);
                }

                //Salto
                if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && IsOnTheGround)
                {
                    DracoRigidbody.AddForce(Vector3.up * UpSpeed, ForceMode.Impulse);
                    //Evitamos doble salto
                    GameManagerAudioSource.PlayOneShot(Jumping);
                    IsOnTheGround = false;
                }

                //Vuelo
                if (Input.GetKey(KeyCode.Q) && GameManagerScript.FlybarCounter > 0)
                {
                    DracoRigidbody.velocity = Vector3.up * FlySpeed + DracoRigidbody.velocity.x * Vector3.right;
                    IsOnTheGround = false;

                    //Tiempo de vuelo
                    CurrentTime += Time.deltaTime;
                    AntiTime = MaxFlyTime - CurrentTime;
                    GameManagerScript.FlybarCounter = AntiTime / MaxFlyTime;
                    GameManagerScript.Flybar.fillAmount = GameManagerScript.FlybarCounter;
                }

                //Fuego
                if (Input.GetKeyDown(KeyCode.E))
                {

                    StartCoroutine(FireCooldown());
                }

                //Agacharse
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    //Insertar animaci�n sprite
                }

                //L�mite cielo
                if (transform.position.y >= SkyLimit)
                {
                    transform.position = new Vector3(transform.position.x, SkyLimit, transform.position.z);
                }
            }     
            
        }
        
    }

    public void OnCollisionEnter(Collision otherCollider)
    {
        //Si colisiona contra el suelo el jugador puede volver a saltar
        if (otherCollider.gameObject.CompareTag("Ground"))
        {
            IsOnTheGround = true;
        }

        if (otherCollider.gameObject.CompareTag("Wall"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

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
            Debug.Log("Cuidao que te pinsho drag�n de mierda");
            //AddForce rebote, hay que calcular a lo 100tifiko otro d�a
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

        if(GameOver == true) //si muero, paro la m�sica y pongo el sonido de muerte
        {
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Pause();
            GameManagerAudioSource.PlayOneShot(GameOverSound);
        }

        
    }

    public void OnTriggerEnter(Collider otherTrigger)
    {
        //Actualizamos el n�mero de monedas recogidas
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

        //Da�o de los enemigos al jugador (proyectil)
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

    //Actualizamos la imagen seg�n la vida del jugador
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

    //Cooldown del fuego, el jugador solo podr� disprar cada 0.5 segundos
    public IEnumerator FireCooldown()
    {
        float FireTimer = 0.5f;
        if (ShootFire == true)
        {
            Instantiate(FuegoPrefab, transform.position, transform.rotation);
            GameManagerAudioSource.PlayOneShot(FireSound);
            ShootFire = false;
        }
        yield return new WaitForSeconds(FireTimer);
        ShootFire = true;
    }


}
