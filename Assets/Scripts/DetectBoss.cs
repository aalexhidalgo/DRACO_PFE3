using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DetectBoss : MonoBehaviour
{
    public static DetectBoss SharedInstance;

    private BossLogic BossLogicScript;
    private SpawnManager SpawnManagerScript;
    private GameManager GameManagerScript;
    private GameObject LifeBoss;

    public GameObject CajaDialogo;

    public bool Damage = false;

    private void Awake()
    {
        SharedInstance = this;
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Player"))
        {
            BossLogicScript = GameObject.Find("Boss").GetComponent<BossLogic>();
            SpawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
            BossLogicScript.enabled = true;
            SpawnManagerScript.enabled = true;
            //DataPersistance.DracoState.FireballValue = PlayerPrefs.GetFloat("Fireball_Value");
            LifeBoss.SetActive(true);
            Damage = true;
        }
    }

    private void Start()
    {
        //DataPersistance.DracoState.FireballValue = 0;
        LifeBoss = GameObject.Find("LifeBoss");
        LifeBoss.SetActive(false);

        GameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameManagerScript.FlybarCounter = 0;
    }

}
