using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarLogic : MonoBehaviour
{
    private bool Active = false;
    private Animator StarAnimator;

    void Start()
    {
        StarAnimator = GetComponent<Animator>();
    }

    public IEnumerator StarAnim()
    {
        Debug.Log("Hola, no estoy roto");
        yield return new WaitForSeconds(1f);
        Active = true;
        StarAnimator.SetBool("IsActive", Active);
        Active = false;
        Destroy(gameObject);
    }
}
