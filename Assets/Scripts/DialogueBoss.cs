using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueBoss : MonoBehaviour
{
    public GameObject CajaDialogo;
    private Rigidbody PlayerRigidbody;
    private PlayerController playerControllerScript;

    void Start()
    {
        CajaDialogo.SetActive(false);
        PlayerRigidbody = GameObject.Find("Player").GetComponent<Rigidbody>();
        playerControllerScript = FindObjectOfType<PlayerController>();
    }
    public IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(2f);
        CajaDialogo.SetActive(true);
        float Alphavalue = 0;

        GameObject child = CajaDialogo.transform.GetChild(0).gameObject;
        Image childImage = child.GetComponent<Image>();
        Color boxColor = childImage.color;

        TextMeshProUGUI textAlpha = child.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

        textAlpha.alpha = Alphavalue;

        while (Alphavalue <= 1)
        {
            boxColor.a = Alphavalue;
            childImage.color = boxColor;

            textAlpha.alpha = Alphavalue;

            Alphavalue += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        boxColor.a = Alphavalue;
        childImage.color = boxColor;

        textAlpha.alpha = Alphavalue;
    }

    public IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1f);
        CajaDialogo.SetActive(true);
        float Alphavalue = 1;

        GameObject child = CajaDialogo.transform.GetChild(0).gameObject;
        Image childImage = child.GetComponent<Image>();
        Color boxColor = childImage.color;

        TextMeshProUGUI textAlpha = child.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

        textAlpha.alpha = Alphavalue;

        while (Alphavalue >= 0)
        {
            boxColor.a = Alphavalue;
            childImage.color = boxColor;

            textAlpha.alpha = Alphavalue;

            Alphavalue -= 0.1f;
            yield return new WaitForSeconds(0.08f);
        }

        boxColor.a = Alphavalue;
        childImage.color = boxColor;

        textAlpha.alpha = Alphavalue;
        playerControllerScript.DracoCanMov = true;
        CajaDialogo.SetActive(false);
    }
    public void OnTriggerEnter(Collider otherTrigger)
    {
        if (otherTrigger.gameObject.CompareTag("Player"))
        {
            StartCoroutine("FadeIn");
        }
    }

    public void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Player"))
        {
            playerControllerScript.DracoCanMov = false;
            StartCoroutine("FadeOut");
        }
    }
}
