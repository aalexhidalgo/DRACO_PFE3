using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreditsLogic : MonoBehaviour
{
    //Maybe incluir alguna cinemática o animación
    public GameObject FinalTextBox;
    public string OriginalMessage;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    //Salimos del juego
    public void ExitButton()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    private IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(2);
        float Alphavalue = 0;

        GameObject child =  FinalTextBox.transform.GetChild(0).gameObject;
        Image childImage = child.GetComponent<Image>();
        Color boxColor = childImage.color;

        TextMeshProUGUI textAlpha = child.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

        textAlpha.alpha = Alphavalue;

        while (Alphavalue <= 1)
        {
            boxColor.a = Alphavalue;
            childImage.color = boxColor;

            textAlpha.alpha = Alphavalue;

            Alphavalue += 0.075f;
            yield return new WaitForSeconds(0.1f);
        }

        boxColor.a = Alphavalue;
        childImage.color = boxColor;

        textAlpha.alpha = Alphavalue;

        yield return new WaitForSeconds(2f);

        foreach (var d in OriginalMessage)
        {
            textAlpha.text += d;
            yield return new WaitForSeconds(0.1f);
        }

    }

    private IEnumerator WaitForSeconds(int Timer)
    {
        yield return new WaitForSeconds(Timer);
    }
}
