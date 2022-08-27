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
            StarLogicScript.StartCoroutine(StarLogicScript.StarAnim());
        }
    }

    /*public void OnTriggerStay(Collider otherTrigger)
    {
        if (otherTrigger.gameObject.CompareTag("Initial_Star"))
        {
            StarLogicScript.StartCoroutine(StarLogicScript.StarAnim());
        }
    }
    */
}
