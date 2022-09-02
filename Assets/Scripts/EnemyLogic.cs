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

            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        PlayerControllerScript.PacificRoute = 0;
        PlayerControllerScript.KilledEnemies += 1;
        if(IsSlums == true)
        {
            PlayerControllerScript.HasKilledSlums = 1;
        }
        Debug.Log("Has matado, ya no eres puro de corazón como Goku jeje");
    }
}
