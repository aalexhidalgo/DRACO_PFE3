using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    private AudioSource AudioManagerAudiosource;
    public AudioClip RockCrash;

    public ParticleSystem RockParticleSystem;
    void Start()
    {
        AudioManagerAudiosource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider otherTrigger)
    {
        if (otherTrigger.gameObject.CompareTag("Ground"))
        {
            Instantiate(RockParticleSystem, gameObject.transform.position, gameObject.transform.rotation);
            AudioManagerAudiosource.PlayOneShot(RockCrash);
            Destroy(gameObject);
            //Meter partículas
        }
        
    }

}
