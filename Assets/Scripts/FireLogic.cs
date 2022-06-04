using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLogic : MonoBehaviour
{
    public float Speed = 10f;
    private EnemyLogic EnemyLogicScript;

    //Particles
    public ParticleSystem RockParticleSystem;
    public ParticleSystem SmokeParticleSystem;

    void Start()
    {
        
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
            //Instantiate(SmokeParticleSystem, otherTrigger.gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }

        if (otherTrigger.gameObject.CompareTag("Rock"))
        {
            
            Destroy(otherTrigger.gameObject);
            Instantiate(RockParticleSystem, otherTrigger.gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }

    }
}
