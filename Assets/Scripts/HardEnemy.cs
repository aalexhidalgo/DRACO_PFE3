using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardEnemy : MonoBehaviour
{

    public GameObject Bullet;
    private GameObject BulletPoint;
    public Animator HardEnemyAnim;

    public float AttackRange = 7f;
    public bool PlayerinAttackRange;
    [SerializeField] private LayerMask PlayerLayer;

    private float ForwardForce = 8f;
    private float UpForce = 7f;
    private bool CanAttack = true;

    //Script

    private GameManager GameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        BulletPoint = GameObject.Find("BulletPoint");
        GameManagerScript = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Pos = transform.position;

        PlayerinAttackRange = Physics.CheckSphere(Pos, AttackRange, PlayerLayer);
        if (PlayerinAttackRange)
        {
            Attack();
        }
        else
        {
            HardEnemyAnim.SetBool("Throw", false);
        }

        if(GameManagerScript.pause == false)
        {
            HardEnemyAnim.SetBool("Throw", true);
        }

        if(GameManagerScript.pause == true)
        {
            HardEnemyAnim.SetBool("Throw", false);
        }
    }

    public void Attack()
    {
        Quaternion BulletRotation = Quaternion.Euler(0f, 90f, 0f);
        if (CanAttack && GameManagerScript.pause == false)
        {
            // Disparamos bala con físicas
            HardEnemyAnim.SetBool("Throw", true);
            Rigidbody rb = Instantiate(Bullet, BulletPoint.transform.position, BulletRotation).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * ForwardForce, ForceMode.Impulse);
            rb.AddForce(transform.up * UpForce, ForceMode.Impulse);

            // Activamos Attack Cooldown
            CanAttack = false;
            StartCoroutine(AttackCooldown());
        }
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(2f);
        CanAttack = true;
    }
}
