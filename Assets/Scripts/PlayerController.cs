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

    private float HorizontalInput;

    private float YRotationLimit = 180;

    private Rigidbody DracoRigidbody;
    public float GravityModifier = 3f;

    public GameObject FuegoPrefab;

    //Contadores Props
    public float CurrentLive = 3;
    private int Multiply = 2;
    public int MoneyCounter = 0;
    private float MaxFlyTime = 2f; //Max_S
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

    // Start is called before the first frame update
    void Start()
    {
        GameOver = false;

        DracoRigidbody = GetComponent<Rigidbody>();
        Physics.gravity *= GravityModifier;

        PropLogicScript = FindObjectOfType<PropLogic>();
        EnemyLogicScript = FindObjectOfType<EnemyLogic>();
        GameManagerScript = FindObjectOfType<GameManager>();

        FlySpeed = FlySpeed + Physics.gravity.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameOver == false)
        {
            //Controladores principales de DRACO

            //Movimiento frontal de DRACO, derecha, izquierda o bien A D
            HorizontalInput = Input.GetAxis("Horizontal");
            DracoRigidbody.AddForce(Vector3.forward * Speed * HorizontalInput);

            //Invertimos su escala con tal de que si avanzamos hacia la izquierda nuestro personaje no va de espaldas hacia esa dirección
            if (HorizontalInput < 0)
            {
                transform.rotation = Quaternion.Euler(0, YRotationLimit, 0);
            }
            if (HorizontalInput > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            //Salto
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && IsOnTheGround)
            {
                DracoRigidbody.AddForce(Vector3.up * UpSpeed, ForceMode.Impulse);
                //Evitamos doble salto
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
                //Insertar animación sprite
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

        //Si jugaor pierde vida si colisiona contra un enemigo
        if (otherCollider.gameObject.CompareTag("Enemy"))
        {
            CurrentLive -= 0.5f;
            Debug.Log("Cuidao que te pinsho dragón de mierda");
            //AddForce rebote, hay que calcular a lo 100tifiko otro día
            UpdateLife();

            if (CurrentLive <= 0)
            {
                CurrentLive = 0;
                Debug.Log("Sa matao Paco");
                GameOver = true;
                GameOverPanel.SetActive(true);
            }
        }
        
    }

    public void OnTriggerEnter(Collider otherTrigger)
    {
        //Actualizamos el número de monedas recogidas
        if (otherTrigger.gameObject.CompareTag("Money"))
        {
            Destroy(otherTrigger.gameObject);
            MoneyCounter += 5;
            Debug.Log($"Tienes {MoneyCounter} monedas, crack");
            UpdateMoney();
        }

        //Actualizamos la vida del jugador
        if (otherTrigger.gameObject.CompareTag("Live"))
        {
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

        //Daño de los enemigos al jugador (proyectil)
        if (otherTrigger.gameObject.CompareTag("EnemyDamage"))
        {
            Destroy(otherTrigger.gameObject);
            CurrentLive -= 0.5f;
            Debug.Log($"Tienes {CurrentLive} vidas, crack");

            if (CurrentLive <= 0)
            {
                CurrentLive = 0;
                Debug.Log("Sa matao Paco");
                GameOver = true;
                GameOverPanel.SetActive(true);
            }

            UpdateLife();
        }

        //Si recogemos una nube, permite volar al jugador
        if (otherTrigger.gameObject.CompareTag("Cloud"))
        {
            GameManagerScript.Flybar.fillAmount = 1;
            GameManagerScript.FlybarCounter = 1;
            Destroy(otherTrigger.gameObject);
        }
    }

    //Actualizamos la imagen según la vida del jugador
    public void UpdateLife()
    {
       float CurrentImage = CurrentLive * Multiply;
       GameManagerScript.LifeImage.sprite = GameManagerScript.LifeSprites[(int)CurrentImage];
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
            Instantiate(FuegoPrefab, transform.position, transform.rotation);
            ShootFire = false;
        }
        yield return new WaitForSeconds(FireTimer);
        ShootFire = true;
    }


}
