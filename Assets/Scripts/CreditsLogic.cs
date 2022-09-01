using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Localization;

public class CreditsLogic : MonoBehaviour
{
    //Maybe incluir alguna cinemática o animación
    public GameObject FinalTextBox;
    public TextMeshProUGUI textAlpha;
    public TextMeshProUGUI NombresText;
    public TextMeshProUGUI GraciasText;
    public string OriginalMessage;

    public LocalizedString[] DialogueLocalize;
    public string[] LocalizeStrings;

    private CreditsPlayer CreditsPlayerScript;

    public GameObject[] Canvas;
    public int canvaIndex = 0;

    public ParticleSystem[] FireWorks;

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

        StartCoroutine(Credits());

    }

    private IEnumerator Credits()
    {
        OriginalMessage = DialogueLocalize[0].GetLocalizedString(LocalizeStrings[0]);
        yield return new WaitForSeconds(2f);

        foreach (var d in OriginalMessage)
        {
            textAlpha.text += d;
            yield return new WaitForSeconds(0.09f);
        }

        yield return new WaitForSeconds(3.5f);

        Canvas[canvaIndex].SetActive(false);
        canvaIndex++;
        Canvas[canvaIndex].SetActive(true);

        textAlpha.text ="";
        OriginalMessage = DialogueLocalize[1].GetLocalizedString(LocalizeStrings[1]);
        yield return new WaitForSeconds(1f);


        foreach (var d in OriginalMessage)
        {
            textAlpha.text += d;
            yield return new WaitForSeconds(0.09f);
        }

        yield return new WaitForSeconds(3.5f);

        Canvas[canvaIndex].SetActive(false);
        canvaIndex++;
        Canvas[canvaIndex].SetActive(true);

        textAlpha.text ="";
        OriginalMessage = DialogueLocalize[2].GetLocalizedString(LocalizeStrings[2]);
        yield return new WaitForSeconds(1f);

        foreach (var d in OriginalMessage)
        {
            textAlpha.text += d;
            yield return new WaitForSeconds(0.09f);
        }

        yield return new WaitForSeconds(3.5f);

        Canvas[canvaIndex].SetActive(false);
        canvaIndex++;
        Canvas[canvaIndex].SetActive(true);

        textAlpha.text ="";
        OriginalMessage = DialogueLocalize[3].GetLocalizedString(LocalizeStrings[3]);
        yield return new WaitForSeconds(1f);

        foreach (var d in OriginalMessage)
        {
            textAlpha.text += d;
            yield return new WaitForSeconds(0.09f);
        }

        yield return new WaitForSeconds(3.5f);
        StartCoroutine(FadeOut());

    }

    private IEnumerator FadeInNombres()
    {
        yield return new WaitForSeconds(2f);
        float Alphavalue = 0;

        NombresText.alpha = Alphavalue;

        while (Alphavalue <= 1)
        {
            NombresText.alpha = Alphavalue;

            Alphavalue += 0.075f;
            yield return new WaitForSeconds(0.1f);
        }

        NombresText.alpha = Alphavalue;

        yield return new WaitForSeconds(2.25f);

        float AlphaValue = 0;
        GraciasText.alpha = AlphaValue;

        while (AlphaValue <= 1)
        {
            GraciasText.alpha = AlphaValue;

            AlphaValue += 0.075f;
            yield return new WaitForSeconds(0.1f);
        }

        GraciasText.alpha = AlphaValue;

        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1f);
        float Alphavalue = 1;

        GameObject child = FinalTextBox.transform.GetChild(0).gameObject;
        Image childImage = child.GetComponent<Image>();
        Color boxColor = childImage.color;

        textAlpha.alpha = Alphavalue;

        while (Alphavalue >= 0)
        {
            boxColor.a = Alphavalue;
            childImage.color = boxColor;

            textAlpha.alpha = Alphavalue;

            Alphavalue -= 0.075f;
            yield return new WaitForSeconds(0.1f);
        }

        boxColor.a = Alphavalue;
        childImage.color = boxColor;

        textAlpha.alpha = Alphavalue;
        CreditsPlayerScript.CanWalk = false;

        foreach (ParticleSystem particle in FireWorks)
        {
            particle.Play();
            yield return new WaitForSeconds(0.50f);
        }
        yield return new WaitForSeconds(0.5f);

        Canvas[canvaIndex].SetActive(false);
        canvaIndex++;
        Canvas[canvaIndex].SetActive(true);


        StartCoroutine(FadeInNombres());
    }

    private IEnumerator WaitForSeconds(int Timer)
    {
        yield return new WaitForSeconds(Timer);
    }
}
