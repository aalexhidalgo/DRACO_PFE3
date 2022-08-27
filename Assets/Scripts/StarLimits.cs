using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarLimits : MonoBehaviour
{
    private StarLogic StarLogicScript;

    void Start()
    {
        StarLogicScript = FindObjectOfType<StarLogic>();
    }
    public void OnTriggerEnter(Collider otherTrigger)
    {
        if (otherTrigger.gameObject.CompareTag("Star"))
        {
            //obj.StartCoroutine(obj.Counter());
            //StarLogic.StartCoroutine(StarLogic.StarAnim());
            Destroy(otherTrigger.gameObject);
        }
    }
}
