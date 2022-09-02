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
            StartCoroutine("WaitingTime");
        }
    }

    private void Start()
    {
        //DataPersistance.DracoState.FireballValue = 0;
        LifeBoss = GameObject.Find("LifeBoss");
        LifeBoss.SetActive(false);

        GameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameManagerScript.FlybarCounter = 0;

        Debug.Log($"Has muerto luchando contra King: {DataPersistance.DeadInBattle == 1}");
    }

    public IEnumerator WaitingTime()
    {
        yield return new WaitForSeconds(2.5f);
        BossLogicScript.enabled = true;
        SpawnManagerScript.enabled = true;
        //DataPersistance.DracoState.FireballValue = PlayerPrefs.GetFloat("Fireball_Value");
        LifeBoss.SetActive(true);
        Damage = true;
    }
}
