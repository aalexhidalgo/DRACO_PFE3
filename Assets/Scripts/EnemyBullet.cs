using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float Speed = 5f;
    private float ZLimit = 8f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z < ZLimit)
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }
        if(transform.position.z > ZLimit)
        {
            transform.Translate(Vector3.back * Speed * Time.deltaTime);
        }       
    }
}
