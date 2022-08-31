using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    private AudioSource GameManagerAudiosource;
    public AudioClip RockCrash;

    public ParticleSystem RockParticleSystem;
    void Start()
    {
        GameManagerAudiosource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider otherTrigger)
    {
        if (otherTrigger.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
            //Meter partículas
        }
        
    }

    private void OnDestroy()
    {
        GameManagerAudiosource.PlayOneShot(RockCrash);
        Instantiate(RockParticleSystem,gameObject.transform.position, gameObject.transform.rotation);
    }

}
