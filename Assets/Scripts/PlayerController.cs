using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    //Hola Tomás, si lees esto dimelo por whatsapp, hackear está mal, lo sabías? No se si entenderás algo, pero adelante, mira

    #region Draco Movement Variables
    //Controladores de DRACO
    public float Speed = 8f;
    public float UpSpeed = 20f;
    public float FlySpeed = 3f;
    public float HorizontalInput;
    private float YRotation = 90;
    //private float SkyLimit = 16.5f;
    private Rigidbody DracoRigidbody;
    private Vector3 NewGravity = new Vector3 (0f, -29.4f, 0f);

    public bool canShoot = true;
    #endregion

    public GameObject FuegoPrefab;
    
    private Animator PlayerAnimator;

    //Contadores Props
    public float CurrentLive = 3;
    private int Multiply = 2;

    public int Shield = 0;
    public int MaxShieldValue = 1;

    public int MoneyCounter = 0;

    private float MaxFlyTime = 0.5f; //Max_S
    private float CurrentTime;  //Timepassed(S)
    private float AntiTime;

    //Booleanas de condiciones
    public bool IsOnTheGround;
    public bool CanFly;
    public bool ShootFire;
    private bool IsFlying;
    private bool CanWalk = true;
    private bool jump = false;
    public bool DracoCanMov;

    //Comunicación con scripts
    private GameManager GameManagerScript;
    private GamePadController gamePadControllerScript;
    //Logros
    /*
    public int ThisLevelCoins;

    public int PacificRoute;
    public int KilledEnemies;
    public int HasKilledSlums;

    public int FireBallCounter;

    public int ItemCounter;

    public int BulletCounter;
    public int MediumCounter;
    */
    #region Audio

    //AudioSources para acceder a sonidos
    public AudioSource GameManagerAudioSource;
    //AudioClips
    public AudioClip Jumping; //funciona
    public AudioClip GameOverSound; //funciona
    public AudioClip FireSound; //funciona
    public AudioClip CoinSound; //funciona
    public AudioClip RockExplotion; //funciona
    public AudioClip RecogerItem; //funciona
    #endregion

    private void Awake()
    {
        canShoot = true;
    }
    void Start()
    {
        gamePadControllerScript = FindObjectOfType<GamePadController>();

        if (DataPersistance.CurrentLevel == 1)
        {
            DracoCanMov = false;
        }
        else
        {
            DracoCanMov = true;
        }
        //RigidBody y Animator
        DracoRigidbody = GetComponent<Rigidbody>();
        Physics.gravity = NewGravity;
        PlayerAnimator = GetComponent<Animator>();

        //Find
        GameManagerScript = FindObjectOfType<GameManager>();
        //GameManagerAudioSource = GameObject.Find("GameManager").GetComponent<AudioSource>();

        //Shield Fly y Money inicial
        MaxShieldValue = DataPersistance.ShieldValue + 1;
        MaxFlyTime = DataPersistance.FlyValue;
        FlySpeed = FlySpeed + Physics.gravity.magnitude;
        MoneyCounter = PlayerPrefs.GetInt("Money_Counter");
        UpdateShield();
        UpdateShieldImage();

        
    }

    void Update()
    {
        if(GameManagerScript.GameOver == false)
        {
            IsWalking();
            PlayerAnimator.SetBool("IsJumping", !IsOnTheGround);
            PlayerAnimator.SetBool("IsFlying", IsFlying);

            //Controladores principales de DRACO

            #region Movimiento Draco
            //Movimiento horizontal
            //TECLADO: RightArrow, LeftArrow o bien A D.
            //GAMEPAD: Joystick Axis X (izquierdo), 3rd Axis Joystick (derecho)
            if(CanWalk && DracoCanMov)
            {
                if(gamePadControllerScript.Xbox_One_Controller == 0) //Si no usas mando de Xbox detecta si usas flechas, joysticks y cruceta de Play
                {
                    HorizontalInput = Input.GetAxisRaw("Horizontal");
                }
                else //Si usas Xbox solo se moverá con los Joysticks y cruceta del mando de Xbox
                {
                    HorizontalInput = Input.GetAxisRaw("Horizontal_Xbox");
                }
            }

            else if(!DracoCanMov)
            {
                HorizontalInput = 0;
            }
            
            else
            {
                HorizontalInput = 1;
            }
            //DracoRigidbody.AddForce(Vector3.forward * Speed * HorizontalInput);
            #endregion

            #region Giro Draco
            //Invertimos su escala con tal de que si avanzamos hacia la izquierda nuestro personaje no va de espaldas hacia esa dirección
            if (HorizontalInput < 0)
            {
                transform.rotation = Quaternion.Euler(0, YRotation, 0);
            }
            if (HorizontalInput > 0)
            {

                transform.rotation = Quaternion.Euler(0, -YRotation, 0);
            }
            #endregion

            #region Salto Draco
            //Salto
            //TECLADO: Spacebar.
            //GAMEPAD: Joystick button 1 (X).

            if ((Input.GetButtonDown("UpMove") && IsOnTheGround && UpSpeed > 0) && gamePadControllerScript.Xbox_One_Controller == 0) //X, Axis
            {
                //DracoRigidbody.AddForce(Vector3.up * UpSpeed, ForceMode.Impulse);
                //Evitamos doble salto
                jump = true;
                GameManagerAudioSource.PlayOneShot(Jumping);
                
                IsOnTheGround = false;
            }

            if (Input.GetButtonDown("UpMove_Xbox") && IsOnTheGround && UpSpeed > 0 && gamePadControllerScript.Xbox_One_Controller == 1) //X, Axis
            {
                //DracoRigidbody.AddForce(Vector3.up * UpSpeed, ForceMode.Impulse);
                //Evitamos doble salto
                jump = true;
                GameManagerAudioSource.PlayOneShot(Jumping);

                IsOnTheGround = false;
            }

            #endregion

            #region Vuelo Draco
            //Vuelo
            //TECLADO: Q.
            //GAMEPAD: Joystick button 7.
            if ((Input.GetButton("FlyMove") && GameManagerScript.FlybarCounter > 0) && gamePadControllerScript.Xbox_One_Controller == 0)
            {
                DracoRigidbody.velocity = Vector3.up * FlySpeed + DracoRigidbody.velocity.x * Vector3.right;
                IsOnTheGround = false;
                IsFlying = true;
                PlayerAnimator.SetBool("IsFlying", IsFlying);

                //Tiempo de vuelo
                CurrentTime += Time.deltaTime;
                AntiTime = MaxFlyTime - CurrentTime;
                GameManagerScript.FlybarCounter = AntiTime / MaxFlyTime;
                GameManagerScript.Flybar.fillAmount = GameManagerScript.FlybarCounter;
            }

            if ((Input.GetButton("FlyMove_Xbox") && GameManagerScript.FlybarCounter > 0) && gamePadControllerScript.Xbox_One_Controller == 1)
            {
                DracoRigidbody.velocity = Vector3.up * FlySpeed + DracoRigidbody.velocity.x * Vector3.right;
                IsOnTheGround = false;
                IsFlying = true;
                PlayerAnimator.SetBool("IsFlying", IsFlying);

                //Tiempo de vuelo
                CurrentTime += Time.deltaTime;
                AntiTime = MaxFlyTime - CurrentTime;
                GameManagerScript.FlybarCounter = AntiTime / MaxFlyTime;
                GameManagerScript.Flybar.fillAmount = GameManagerScript.FlybarCounter;
            }
            #endregion

            #region Fuego
            //Fuego
            //TECLADO: E.
            //GAMEPAD: Joystick button 2.
            if ((Input.GetButtonDown("Fire") && canShoot) && gamePadControllerScript.Xbox_One_Controller == 0)
            {
                StartCoroutine(FireCooldown());
            }

            if ((Input.GetButtonDown("Fire_Xbox") && canShoot) && gamePadControllerScript.Xbox_One_Controller == 1)
            {
                StartCoroutine(FireCooldown());
            }
            #endregion

            if (IsOnTheGround == true)
            {
                IsFlying = false;
            }
        }                  
    }

    void FixedUpdate()
    {
        DracoRigidbody.AddForce(Vector3.forward * Speed * HorizontalInput, ForceMode.Impulse);
        if (jump)
        {
            DracoRigidbody.AddForce(Vector3.up * UpSpeed, ForceMode.Impulse);
            jump = false;
        }
    }

    private void IsWalking()
    {
        if (IsOnTheGround == true && HorizontalInput != 0)
        {
            PlayerAnimator.SetBool("IsWalking", true);
        }
        else
        {
            PlayerAnimator.SetBool("IsWalking", false);
        }
    }

    public void OnCollisionEnter(Collision otherCollider)
    {
        //Si colisiona contra el suelo el jugador puede volver a saltar
        if (otherCollider.gameObject.CompareTag("Ground"))
        {
            IsOnTheGround = true;
        }

        if(otherCollider.gameObject.CompareTag("Lava") || otherCollider.gameObject.CompareTag("Water"))
        {
            CurrentLive = 0;
            UpdateLife();
            GameManagerScript.GameOver = true;
        }

        #region Collide Enemy
        //Si jugador pierde vida si colisiona contra un enemigo
        if (otherCollider.gameObject.CompareTag("Enemy") && Shield == 0)
        {
            CurrentLive -= 0.5f;
            //Debug.Log("Cuidao que te pinsho dragón de mierda");
            //AddForce rebote, hay que calcular a lo 100tifiko otro día
            if (CurrentLive <= 0)
            {
                CurrentLive = 0;
                //Debug.Log("Sa matao Paco");
                GameManagerScript.GameOver = true;
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

        #endregion

        if (GameManagerScript.GameOver == true) //si muero, paro la música y pongo el sonido de muerte
        {
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Pause();
            GameManagerAudioSource.PlayOneShot(GameOverSound);
        }    
    }


    public void OnTriggerStay(Collider otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("AutoWalk"))
        {
            CanWalk = false;
        }
    }

    public void OnTriggerExit(Collider otherCollider)
    {
        if(otherCollider.gameObject.CompareTag("AutoWalk"))
        {
            CanWalk = true;
            DracoRigidbody.velocity = Vector3.zero;
            Destroy(otherCollider.gameObject, 2.1f);
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
            DataPersistance.CoinsColected += 5;
            //Debug.Log($"Tienes {MoneyCounter} monedas, crack");
            UpdateMoney();
        }

        #region Collide Live
        //Actualizamos la vida del jugador
        if (otherTrigger.gameObject.CompareTag("Live"))
        {
            GameManagerAudioSource.PlayOneShot(RecogerItem);
            CurrentLive++;
            Destroy(otherTrigger.gameObject);
            DataPersistance.ItemsCollected += 1;

            if (CurrentLive >= 3)
            {
                CurrentLive = 3f;
                MoneyCounter += 10;
                //ThisLevelCoins += 10;
                //DataPersistance.CoinsColected += 10;
                UpdateMoney();
                Destroy(otherTrigger.gameObject);
            }
            else
            {
                
                Debug.Log($"Tienes {CurrentLive} vidas, crack");
            }

            UpdateLife();

        }
        #endregion

        #region Collide Spikes
        if (otherTrigger.gameObject.CompareTag("Spike") && Shield == 0)
        {
            CurrentLive = 0;

            Debug.Log("Chanelazo");
            GameManagerScript.GameOver = true;
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Pause();
            GameManagerAudioSource.PlayOneShot(GameOverSound);


            UpdateLife();
        }

        else if (otherTrigger.gameObject.CompareTag("Spike") && Shield == 1)
        {
            Shield = 0;
            UpdateShield();
        }

        #endregion

        #region Collide Bullet
        //Daño de los enemigos al jugador (proyectil)
        if (otherTrigger.gameObject.CompareTag("Bullet") && Shield == 1)
        {
            MaxShieldValue -= 1;
            DataPersistance.Bullets += 1;

            if (MaxShieldValue <= 0)
            {
                Shield = 0;
                UpdateShield();
                //Debug.Log("Te quedas sin escudo crack");
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
            DataPersistance.Bullets += 1;

            if (CurrentLive <= 0)
            {
                CurrentLive = 0;
                //Debug.Log("Quack, quack, quack...");
                GameManagerScript.GameOver = true;
                
                UpdateLife();
            }

            UpdateLife();

        }
        #endregion

        #region PowerUps
        //Si recogemos una nube, permite volar al jugador
        if (otherTrigger.gameObject.CompareTag("Cloud"))
        {            
            GameManagerAudioSource.PlayOneShot(RecogerItem);
            GameManagerScript.Flybar.fillAmount = 1;
            GameManagerScript.FlybarCounter = 1;
            Destroy(otherTrigger.gameObject);
            DataPersistance.ItemsCollected += 1;
        }

        if(otherTrigger.gameObject.CompareTag("Shield"))
        {
            GameManagerAudioSource.PlayOneShot(RecogerItem);
            Shield = 1;
            UpdateShield();
            Destroy(otherTrigger.gameObject);
            DataPersistance.ItemsCollected += 1;
        }
        #endregion

    }

    //Actualizamos la imagen según la vida del jugador
    public void UpdateLife()
    {
       float CurrentImage = CurrentLive * Multiply;
       GameManagerScript.LifeImage.sprite = GameManagerScript.LifeSprites[(int)CurrentImage];
    }

    #region Escudo Draco
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
    #endregion
    //Actualizamos el contador de monedas
    public void UpdateMoney()
    {
        GameManagerScript.MoneyText.text = $"{MoneyCounter}";
    }

    //Cooldown del fuego, el jugador solo podrá disprar cada 0.5 segundos
    public IEnumerator FireCooldown()
    {
        float FireTimer = 0.75f;
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
