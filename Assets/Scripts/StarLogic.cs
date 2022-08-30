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
        float RandomTime = Random.Range(0.5f, 1f);
        yield return new WaitForSeconds(RandomTime);
        StarAnimator.SetBool("Active", true);
        yield return new WaitForSeconds(1f);
    }
}
