using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumEnemy : MonoBehaviour
{
    public Animator MediumEnemyAnim;

    public float MoveRange = 17.5f;
    public bool PlayerinMoveRange;
    [SerializeField] private LayerMask PlayerLayer;

    //Script
    private GameManager GameManagerScript;
    void Start()
    {
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

