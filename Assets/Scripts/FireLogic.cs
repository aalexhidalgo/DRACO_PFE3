using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLogic : MonoBehaviour
{
    public float Speed = 10f;
    private EnemyLogic EnemyLogicScript;

    //Particles
    public ParticleSystem RockParticleSystem;
    public ParticleSystem DamageParticleSystem;

    //Scripts
    private PlayerController PlayerControllerScript;

    void Start()
    {
        PlayerControllerScript = FindObjectOfType<PlayerController>();
        Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Speed * Time.deltaTime);       
    }

    public void OnTriggerEnter(Collider otherTrigger)
    {

        if (otherTrigger.gameObject.CompareTag("Enemy"))
        {
            EnemyLogicScript = otherTrigger.gameObject.GetComponent<EnemyLogic>();
            EnemyLogicScript.EnemyLife-= DataPersistance.DracoState.FireballValue;
            Instantiate(DamageParticleSystem, otherTrigger.gameObject.transform.position, gameObject.transform.rotation);

            Destroy(gameObject);
        }

        if (otherTrigger.gameObject.CompareTag("Rock"))
        {
            PlayerControllerScript.GameManagerAudioSource.PlayOneShot(PlayerControllerScript.RockExplotion);
            Destroy(otherTrigger.gameObject);
            Instantiate(RockParticleSystem, otherTrigger.gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }

        if (otherTrigger.gameObject.CompareTag("Objeto_Mobil"))
        {
            Instantiate(DamageParticleSystem, otherTrigger.gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }

        if (otherTrigger.gameObject.CompareTag("Wall"))
        {
            Instantiate(DamageParticleSystem, otherTrigger.gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }


        if (otherTrigger.gameObject.CompareTag("Ground"))
        {
            Instantiate(DamageParticleSystem, otherTrigger.gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }


    }
    private void OnDestroy()
    {
        Instantiate(DamageParticleSystem, gameObject.transform.position, gameObject.transform.rotation);
    }
}
