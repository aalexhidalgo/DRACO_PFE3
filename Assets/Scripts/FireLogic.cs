using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLogic : MonoBehaviour
{
    public float Speed = 10f;
    private EnemyLogic EnemyLogicScript;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider otherTrigger)
    {

        if (otherTrigger.gameObject.CompareTag("Enemy"))
        {
            EnemyLogicScript = otherTrigger.gameObject.GetComponent<EnemyLogic>();
            EnemyLogicScript.EnemyLife--;
            Destroy(gameObject);
        }
    }
}
