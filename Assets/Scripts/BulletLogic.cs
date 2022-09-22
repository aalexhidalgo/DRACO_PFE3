using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    private AudioSource AudioManagerAudiosource;
    public AudioClip RockCrash;
    public AudioClip HitDraco;

    public ParticleSystem RockParticleSystem;
    public ParticleSystem SmallerRockParticleSystem;
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
        }

        if (otherTrigger.gameObject.CompareTag("Player"))
        {
            Instantiate(SmallerRockParticleSystem, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }

        if (otherTrigger.gameObject.CompareTag("Water"))
        {
            //Instantiate(RockParticleSystem, gameObject.transform.position, gameObject.transform.rotation);
            //AudioManagerAudiosource.PlayOneShot(RockCrash);
            Destroy(gameObject,0.2f);
        }

    }

}
