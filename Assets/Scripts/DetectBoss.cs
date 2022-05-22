using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBoss : MonoBehaviour
{
    private BossLogic BossLogicScript;

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Player"))
        {
            BossLogicScript = GameObject.Find("Boss").GetComponent<BossLogic>();
            BossLogicScript.enabled = true;

        }
    }
}
