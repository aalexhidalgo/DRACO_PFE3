using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBoss : MonoBehaviour
{
    private BossLogic BossLogicScript;
    private GameObject LifeBoss;

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Player"))
        {
            BossLogicScript = GameObject.Find("Boss").GetComponent<BossLogic>();
            BossLogicScript.enabled = true;
            DataPersistance.DracoState.FireballValue = PlayerPrefs.GetFloat("Fireball_Value");
            LifeBoss.SetActive(true);
        }
    }

    private void Start()
    {
        DataPersistance.DracoState.FireballValue = 0;
        LifeBoss = GameObject.Find("LifeBoss");
        LifeBoss.SetActive(false);
    }
}
