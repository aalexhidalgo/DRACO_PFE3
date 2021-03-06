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
        GameManagerAudiosource = GameObject.Find("GameManager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider otherTrigger)
    {
        //Actualizamos el n?mero de monedas recogidas
        if (otherTrigger.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
            //Meter part?culas
        }
        
    }

    private void OnDestroy()
    {
        GameManagerAudiosource.PlayOneShot(RockCrash);
        Instantiate(RockParticleSystem,gameObject.transform.position, gameObject.transform.rotation);
    }

}
