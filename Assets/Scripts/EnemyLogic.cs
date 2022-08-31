using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public float EnemyLife;
    private AudioSource AudioManagerAudiosource;
    public AudioClip DeadSound; //funciona
    //public ParticleSystem SmokeParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        AudioManagerAudiosource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyLife <= 0)
        {
            AudioManagerAudiosource.PlayOneShot(DeadSound);

            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        //Instantiate(SmokeParticleSystem, transform.position, transform.rotation);
    }
}
