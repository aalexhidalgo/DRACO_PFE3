using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Controladores de DRACO
    public float Speed = 8f;
    public float UpSpeed = 20f;
    public float FlySpeed = 8f;

    private float HorizontalInput;

    private float YRotationLimit = 180;

    private Rigidbody DracoRigidbody;
    public float GravityModifier = 3f;

    public bool IsOnTheGround;
    public GameObject FuegoPrefab;

    //Contadores Props
    public float CurrentLive = 3;
    private int Multiply = 2;
    public int MoneyCounter = 0;
    private float MaxFlyTime = 5f; //Max_S
    private float CurrentTime;  //Timepassed(S)

    //Booleanas de condiciones
    public bool CanFly;
    public bool ShootFire;

    //Comunicación con scripts
    private MoneyLogic MoneyLogicScript;
    private EnemyLogic EnemyLogicScript;
    private GameManager GameManagerScript;

    public bool GameOver;

    // Start is called before the first frame update
    void Start()
    {
        DracoRigidbody = GetComponent<Rigidbody>();
        Physics.gravity *= GravityModifier;

        MoneyLogicScript = FindObjectOfType<MoneyLogic>();
        EnemyLogicScript = FindObjectOfType<EnemyLogic>();
        GameManagerScript = FindObjectOfType<GameManager>();

        FlySpeed = FlySpeed + Physics.gravity.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        //Controladores principales de DRACO

        //Movimiento frontal de DRACO, derecha, izquierda o bien A D
        HorizontalInput = Input.GetAxis("Horizontal");
        DracoRigidbody.AddForce(Vector3.forward * Speed * HorizontalInput);

        //Invertimos su escala con tal de que si avanzamos hacia la izquierda nuestro personaje no va de espaldas hacia esa dirección
        if (HorizontalInput < 0)
        {
            transform.rotation = Quaternion.Euler (0, YRotationLimit, 0);
        }
        if (HorizontalInput > 0)
        {
            transform.rotation = Quaternion.Euler (0, 0, 0);
        }

        //Salto
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && IsOnTheGround)
        {
            DracoRigidbody.AddForce(Vector3.up * UpSpeed, ForceMode.Impulse);
            //DracoRigidbody.AddForce(Vector3.down * Gravity, ForceMode.Impulse);
            IsOnTheGround = false;
        }

        //Vuelo
        if (Input.GetKey(KeyCode.Q))
        {
            DracoRigidbody.velocity = Vector3.up * FlySpeed + DracoRigidbody.velocity.x * Vector3.right;
            IsOnTheGround = false;
            CurrentTime -= Time.deltaTime;
            GameManagerScript.FlybarCounter = CurrentTime/MaxFlyTime;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(FireCooldown());
        }

        //Agacharse
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            //Insertar animación
        }
    }

    public void OnCollisionEnter(Collision otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Ground"))
        {
            IsOnTheGround = true;
            Speed = 8f;
        }

        if (otherCollider.gameObject.CompareTag("Enemy"))
        {
            CurrentLive -= 0.5f;
            Debug.Log("Cuidao que te pinsho dragón de mierda");
            //AddForce rebote, hay que calcular a lo 100tifiko otro día
            UpdateLife();
        }
    }

    public void OnTriggerEnter(Collider otherTrigger)
    {
        if (otherTrigger.gameObject.CompareTag("Money"))
        {
            Destroy(otherTrigger.gameObject);
            MoneyCounter += 5;
            Debug.Log($"Tienes {MoneyCounter} monedas, crack");
            UpdateMoney();
        }

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

        //Proyectil

        if (otherTrigger.gameObject.CompareTag("EnemyDamage"))
        {
            Destroy(otherTrigger.gameObject);
            CurrentLive -= 0.5f;
            Debug.Log($"Tienes {CurrentLive} vidas, crack");

            if (CurrentLive <= 0)
            {
                Debug.Log("Sa matao Paco");
                GameOver = true;
            }

            UpdateLife();
        }
    }

    public void UpdateLife()
    {
       float CurrentImage = CurrentLive * Multiply;
       GameManagerScript.LifeImage.sprite = GameManagerScript.LifeSprites[(int)CurrentImage];
    }

    public void UpdateMoney()
    {
        GameManagerScript.MoneyText.text = $"{MoneyCounter}";
    }

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
