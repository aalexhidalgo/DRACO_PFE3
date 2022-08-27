using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarLogic : MonoBehaviour
{
    private Animator StarAnimator;

    void Start()
    {
        StarAnimator = GetComponent<Animator>();
    }

    public IEnumerator StarAnim()
    {
        Debug.Log("Hola, no estoy roto");
        StarAnimator.SetBool("Active", true);
        yield return new WaitForSeconds(0.5f);
        Destroy(transform.parent.gameObject);
    }
}
