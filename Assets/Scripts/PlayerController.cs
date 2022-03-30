using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Controladores de DRACO
    public float Speed = 30f;
    public float UpSpeed = 20f;
    public float FlySpeed = 5f;

    private float HorizontalInput;

    private int ZScale = 1;

    private Rigidbody DracoRigidbody;
    public float GravityModifier = 1.2f;

    public bool IsOnTheGround;
    public GameObject FuegoPrefab;

    // Start is called before the first frame update
    void Start()
    {
        DracoRigidbody = GetComponent<Rigidbody>();
        Physics.gravity *= GravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento frontal de DRACO, derecha, izquierda o bien A D
        HorizontalInput = Input.GetAxis("Horizontal");
        DracoRigidbody.AddForce(Vector3.forward * Speed * HorizontalInput);

        //Invertimos su escala con tal de que si avanzamos hacia la izquierda nuestro personaje no va de espaldas hacia esa dirección
        if (HorizontalInput < 0)
        {
            transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, -ZScale);
        }
        if (HorizontalInput > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, ZScale);
        }
        

        //Salto
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && IsOnTheGround)
        {
            DracoRigidbody.AddForce(Vector3.up * UpSpeed, ForceMode.Impulse);
            IsOnTheGround = false;
        }

        //Vuelo
        if (Input.GetKey(KeyCode.Q))
        {
            DracoRigidbody.AddForce(Vector3.up * FlySpeed);
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(FuegoPrefab, transform.position, FuegoPrefab.transform.rotation);
        }
    }

    public void OnCollisionEnter(Collision otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Ground"))
        {
            IsOnTheGround = true;
        }
    }
}
