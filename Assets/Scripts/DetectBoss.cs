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

    public IEnumerator FadeIn()
    {
        float Alphavalue = 0;

        GameObject child = CajaDialogo.transform.GetChild(0).gameObject;
        Image childImage = child.GetComponent<Image>();
        Color boxColor = childImage.color;

        GameObject DracoImage = child.transform.GetChild(1).gameObject;
        SpriteRenderer ImageDraco = DracoImage.GetComponent<SpriteRenderer>();
        Color DracoColor = ImageDraco.color;

        TextMeshProUGUI textAlpha = child.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

        textAlpha.alpha = Alphavalue;

        while (Alphavalue <= 1)
        {
            boxColor.a = Alphavalue;
            childImage.color = boxColor;

            DracoColor.a = Alphavalue;
            ImageDraco.color = DracoColor;

            textAlpha.alpha = Alphavalue;

            Alphavalue += 0.1f;
            yield return new WaitForSeconds(0.2f);
        }

    }
}
