using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTrigger : MonoBehaviour
{
    public Animator[] EnemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if(otherCollider.gameObject.CompareTag("Player"))
        {
            foreach (Animator anim in EnemyAnimator)
            {
                anim.SetBool("Active", true);
                //anim.SetBool("Throw", true); (medium)
                //anim.SetBool("Blade", true); Hacer una o dos animaciones de espada random para los tochos (hard)
            }
        }
    }
}
