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

    //Offset
    private GameObject Player;
    private float Distance = 20f;

    void Start()
    {
        PlayerControllerScript = FindObjectOfType<PlayerController>();
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float PlayerPosition = Player.transform.position.z;
        transform.Translate(Vector3.right * Speed * Time.deltaTime);
        if ((gameObject.transform.position.z - PlayerPosition) >= Distance)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider otherTrigger)
    {

        if (otherTrigger.gameObject.CompareTag("Enemy"))
        {
            EnemyLogicScript = otherTrigger.gameObject.GetComponent<EnemyLogic>();
            EnemyLogicScript.EnemyLife-= DataPersistance.FireballValue;
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
            Instantiate(DamageParticleSystem, transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }


        if (otherTrigger.gameObject.CompareTag("Ground"))
        {
            Instantiate(DamageParticleSystem, transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }

        if (otherTrigger.gameObject.CompareTag("Shield_Boss"))
        {
            Instantiate(DamageParticleSystem, otherTrigger.gameObject.transform.position, gameObject.transform.rotation);
            Destroy(otherTrigger.gameObject);
        }

        if (otherTrigger.gameObject.CompareTag("Boss"))
        {
            Instantiate(DamageParticleSystem, otherTrigger.gameObject.transform.position, gameObject.transform.rotation);
        }
    }

    private void OnDestroy()
    {
        DataPersistance.Fireballs += 1;
    }
}
