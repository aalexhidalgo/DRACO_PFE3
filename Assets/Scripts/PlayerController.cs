using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Controladores de DRACO
    public float Speed = 8f;
    public float UpSpeed = 20f;
    public float FlySpeed = 5f;

    private float HorizontalInput;

    private float YRotationLimit = 180;

    private Rigidbody DracoRigidbody;
    public float GravityModifier = 3f;

    public bool IsOnTheGround;
    public GameObject FuegoPrefab;

    //Contadores Props
    public int CurrentLive = 3;
    public int MoneyCounter = 0;
    public bool Nube;

    //Comunicación con scripts
    private MoneyLogic MoneyLogicScript;
    private EnemyLogic EnemyLogicScript;
    private GameManager GameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        DracoRigidbody = GetComponent<Rigidbody>();
        Physics.gravity *= GravityModifier;

        MoneyLogicScript = FindObjectOfType<MoneyLogic>();
        EnemyLogicScript = FindObjectOfType<EnemyLogic>();
        GameManagerScript = FindObjectOfType<GameManager>();
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
            Speed = 6f;
            DracoRigidbody.AddForce(Vector3.up * UpSpeed, ForceMode.Impulse);
            IsOnTheGround = false;
        }

        //Vuelo
        if (Input.GetKey(KeyCode.Q))
        {
            DracoRigidbody.velocity = Vector3.up * FlySpeed + DracoRigidbody.velocity.x * Vector3.right;
            IsOnTheGround = false;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(FuegoPrefab, transform.position, transform.rotation);
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
    }

    public void OnTriggerEnter(Collider otherTrigger)
    {
        if (otherTrigger.gameObject.CompareTag("Money"))
        {
            Destroy(otherTrigger.gameObject);
            MoneyCounter += 5;
            Debug.Log($"Tienes {MoneyCounter} monedas, crack");
        }

        if (otherTrigger.gameObject.CompareTag("Live"))
        {
            if (CurrentLive == 3)
            {
                CurrentLive = 3;
                MoneyCounter += 10;
            }
            else
            {
                Destroy(otherTrigger.gameObject);
                CurrentLive++;
                Debug.Log($"Tienes {CurrentLive} vidas, crack");
            }

        }

        if (otherTrigger.gameObject.CompareTag("EnemyDamage"))
        {
            Destroy(otherTrigger.gameObject);
            CurrentLive--;
            Debug.Log($"Tienes {CurrentLive} vidas, crack");
        }
    }

    public void UpdateLife()
    {
       // GameManagerScript.LifeImage = GameManagerScript.LifeSprites[CurrentLive / 0.5f];
    }
}
