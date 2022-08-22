using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreditsLogic : MonoBehaviour
{
    //Maybe incluir alguna cinem�tica o animaci�n
    public GameObject FinalTextBox;
    public TextMeshProUGUI textAlpha;
    public string OriginalMessage;

    private CreditsPlayer CreditsPlayerScript;

    public GameObject[] Canvas;
    public int canvaIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        CreditsPlayerScript = FindObjectOfType<CreditsPlayer>();
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

    }

    private IEnumerator Credits()
    {
        OriginalMessage = "Y as� Draco consigui� restaurar la paz en el reino tras derrotar al malvado King, devolviendo la felicidad a sus habitantes que elogiaron al peque�o drag�n por su gran valent�a.";
        yield return new WaitForSeconds(2f);

        foreach (var d in OriginalMessage)
        {
            textAlpha.text += d;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(4f);

        Canvas[canvaIndex].SetActive(false);
        canvaIndex++;
        Canvas[canvaIndex].SetActive(true);

        OriginalMessage = "A poco a poco fue reuniendo a los pocos dragones que quedaban, regresando tambi�n la convivencia entre especies y la vida que tiempo atr�s caracteriz� a Mugen.";

    }

    private IEnumerator WaitForSeconds(int Timer)
    {
        yield return new WaitForSeconds(Timer);
    }
}
