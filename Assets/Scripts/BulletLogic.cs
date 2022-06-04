using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider otherTrigger)
    {
        //Actualizamos el número de monedas recogidas
        if (otherTrigger.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
            //Meter partículas
        }
    }

}
