using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float Speed = 10f;
    private EnemyLogic EnemyLogicScript;

    void Start()
    {
        EnemyLogicScript = FindObjectOfType<EnemyLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision otherCollider)
    {

        if (otherCollider.gameObject.CompareTag("Enemy"))
        {
            EnemyLogicScript.EnemyLife--;
            Destroy(gameObject);
        }
    }
}
