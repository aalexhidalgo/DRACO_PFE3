using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumEnemy : MonoBehaviour
{
    public Animator MediumEnemyAnim;

    public float MoveRange = 17.5f;
    public bool PlayerinMoveRange;
    [SerializeField] private LayerMask PlayerLayer;

    private bool CanMove = true;

    //Script
    private GameManager GameManagerScript;

    //Sonidos
    private AudioSource GameManagerAudiosource;
    public AudioClip DeadSound;

    void Start()
    {
        GameManagerAudiosource = GameObject.Find("GameManager").GetComponent<AudioSource>();
        MediumEnemyAnim.SetBool("Active", false);
    }

    // Update is called once per frame
    void Update()
    {   
        Vector3 Pos = transform.position;

        PlayerinMoveRange = Physics.CheckSphere(Pos, MoveRange, PlayerLayer);
        if (PlayerinMoveRange)
        {
            MediumEnemyAnim.SetBool("Active", true);
        }     
    }
}

