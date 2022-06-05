using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public float EnemyLife;
    private AudioSource GameManagerAudiosource;
    public AudioClip DeadSound; //funciona
    //public ParticleSystem SmokeParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        GameManagerAudiosource = GameObject.Find("GameManager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyLife <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        //Instantiate(SmokeParticleSystem, transform.position, transform.rotation);
        GameManagerAudiosource.PlayOneShot(DeadSound);
    }
}
