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
    // Start is called before the first frame update
    void Start()
    {
        GameManagerScript = FindObjectOfType<GameManager>();
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

        if (GameManagerScript.pause == false)
        {
            MediumEnemyAnim.SetBool("Active", true);
        }

        if (GameManagerScript.pause == true)
        {
            MediumEnemyAnim.SetBool("Active", false);
        }
    }

    /*public void Move()
    {
        if (CanMove && GameManagerScript.pause == false)
        {
            // El medium activa animación de movimiento
            MediumEnemyAnim.SetBool("Active", true);
        }
    }
    */
}

