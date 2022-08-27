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
        yield return new WaitForSeconds(1f);
        Active = true;
        StarAnimator.SetBool("IsActive", Active);
        Active = false;
    }
}
