using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLogic_Boss : MonoBehaviour
{
    public float Speed = 10f;

    //Particles
    public ParticleSystem DamageParticleSystem;

    //Scripts
    private PlayerController PlayerControllerScript;
    private GameManager GameManagerScript;

    void Start()
    {
        PlayerControllerScript = FindObjectOfType<PlayerController>();
        GameManagerScript = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Speed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider otherTrigger)
    {

        if (otherTrigger.gameObject.CompareTag("Player"))
        {
            if (PlayerControllerScript.Shield == 1)
            {
                PlayerControllerScript.MaxShieldValue -= 1;
                if (PlayerControllerScript.MaxShieldValue <= 0)
                {
                    PlayerControllerScript.Shield = 0;
                    PlayerControllerScript.UpdateShield();
                    Debug.Log("Te quedas sin escudo crack");
                }
                else
                {
                    PlayerControllerScript.UpdateShieldImage();
                }
            }

            else
            {
                PlayerControllerScript.CurrentLive -= 0.5f;               
               
                if (PlayerControllerScript.CurrentLive <= 0)
                {
                    PlayerControllerScript.CurrentLive = 0;
                    PlayerControllerScript.GameOver = true;
                    PlayerControllerScript.GameOverPanel.SetActive(true);
                    PlayerControllerScript.UpdateLife();
                }
                
                PlayerControllerScript.UpdateLife();
                Destroy(gameObject);
            }
        }

        if(otherTrigger.gameObject.CompareTag("Ground"))
        {
            Instantiate(DamageParticleSystem, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }

        if (otherTrigger.gameObject.CompareTag("Wall"))
        {
            Instantiate(DamageParticleSystem, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }

    }

    /*private void OnDestroy()
    {
        Instantiate(DamageParticleSystem, gameObject.transform.position, gameObject.transform.rotation);
    }
    */
}
