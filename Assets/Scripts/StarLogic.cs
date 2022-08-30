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
        yield return new WaitForSeconds(1f);
        StarAnimator.SetBool("Active", true);
        yield return new WaitForSeconds(1f);
        Destroy(transform.parent.gameObject);
    }
}
