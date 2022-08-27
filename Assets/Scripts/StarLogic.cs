using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarLogic : MonoBehaviour
{
    private bool Active = false;

    public void OnTriggerEnter(Collider otherTrigger)
    {
        if(otherTrigger.CompareTag("Star"))
        {
            Active = true;
        }
    }

    public void OnTriggerExit(Collider otherTrigger)
    {
        if (otherTrigger.CompareTag("Star"))
        {
            Destroy(otherTrigger.gameObject);
        }
    }

    public IEnumerator StarAnim()
    {
        yield return new WaitForSeconds(1f);
        //Activar animación: Set blabla
        //Movimiento z (empty antes o script a parte???)
        Active = false;
    }
}
