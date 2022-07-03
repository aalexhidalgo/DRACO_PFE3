using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBoss : MonoBehaviour
{
    private BossLogic BossLogicScript;
    private SpawnManager SpawnManagerScript;
    private GameObject LifeBoss;

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Player"))
        {
            BossLogicScript = GameObject.Find("Boss").GetComponent<BossLogic>();
            SpawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
            BossLogicScript.enabled = true;
            SpawnManagerScript.enabled = true;
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
