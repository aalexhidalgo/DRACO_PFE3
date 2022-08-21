using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsPlayer: MonoBehaviour
{
    public GameObject MovingFloor;
    

    #region Draco Movement Variables
    //Controladores de DRACO
    public float Speed = 1.5f;
    public float UpSpeed = 20f;
    public float HorizontalInput;
    private Rigidbody DracoRigidbody;
    private Vector3 NewGravity = new Vector3 (0f, -29.4f, 0f);
    #endregion
    
    private Animator PlayerAnimator;

    //Booleanas de condiciones
    public bool IsOnTheGround;
    
    private bool CanWalk = false;
    private bool jump = false;
    private bool IsFlying = false;
    public bool DracoCanMov;

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

    void Start()
    {
        if(DataPersistance.CurrentLevel == 1)
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
    }

    void Update()
    {
        IsWalking();
        PlayerAnimator.SetBool("IsJumping", !IsOnTheGround);
        PlayerAnimator.SetBool("IsFlying", IsFlying);

        #region Movimiento Draco
        //Movimiento horizontal
            
        if(!DracoCanMov)
        {
            HorizontalInput = 0;
        }
            
        else
        {
            HorizontalInput = 1;
        }
        //DracoRigidbody.AddForce(Vector3.forward * Speed * HorizontalInput);
        #endregion


    }

    void FixedUpdate()
    {
        DracoRigidbody.AddForce(Vector3.right * Speed * HorizontalInput, ForceMode.Impulse);
        if (jump)
        {
            DracoRigidbody.AddForce(Vector3.up * UpSpeed, ForceMode.Impulse);
            jump = false;
        }
    }

    private void IsWalking()
    {
        PlayerAnimator.SetBool("IsWalking", true);
    }

    public void OnCollisionEnter(Collision otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Ground"))
        {
            IsOnTheGround = true;
        }
    }


    public void OnTriggerStay(Collider otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("AutoWalk"))
        {
            DracoCanMov = true;
            CanWalk = false;
        }
    }

    public void OnTriggerExit(Collider otherCollider)
    {
        if(otherCollider.gameObject.CompareTag("AutoWalk"))
        {
            DracoCanMov = false;
            Destroy(otherCollider.gameObject);
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
        }
    }
    public void OnTriggerEnter(Collider otherTrigger)
    {
        //Actualizamos el número de monedas recogidas
        if (otherTrigger.gameObject.CompareTag("Money"))
        {
            GameManagerAudioSource.PlayOneShot(CoinSound);  
            Destroy(otherTrigger.gameObject);
        }

        #region PowerUps
        //Si recogemos una nube, permite volar al jugador
        if (otherTrigger.gameObject.CompareTag("Cloud"))
        {
            GameManagerAudioSource.PlayOneShot(RecogerItem);
            Destroy(otherTrigger.gameObject);
        }

        if(otherTrigger.gameObject.CompareTag("Shield"))
        {
            GameManagerAudioSource.PlayOneShot(RecogerItem);
            Destroy(otherTrigger.gameObject);
        }
        #endregion

    }

    public void MoveScenario()
    {
        MovingFloor.transform.Translate(Vector3.up * 5 * Time.deltaTime);
        if(MovingFloor.transform.position.x <= -500)
        {
            MovingFloor.transform.position = new Vector3(0, MovingFloor.transform.position.y, MovingFloor.transform.position.z);
        }
    }
}
