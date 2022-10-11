using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public float EnemyLife;
    private AudioSource AudioManagerAudiosource;
    public AudioClip DeadSound;
    private PlayerController PlayerControllerScript;
    public bool IsSlums;

    void Start()
    {
        AudioManagerAudiosource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
        PlayerControllerScript = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (EnemyLife <= 0)
        {
            AudioManagerAudiosource.PlayOneShot(DeadSound);
            //DataPersistance.PacificRoute = 0;
            PlayerControllerScript.pacificRoute = 0;
            //DataPersistance.KilledEnemies += 1;
            PlayerControllerScript.enemyCounter += 1;

            if (IsSlums == true)
            {
                DataPersistance.HasKilledSlums = 1;
            }
            Debug.Log("Has matado, ya no eres puro de corazón como Goku jeje");
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        
    }
}
