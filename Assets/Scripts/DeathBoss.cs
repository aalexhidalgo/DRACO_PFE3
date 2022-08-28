using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Localization;

public class DeathBoss : MonoBehaviour
{
    public GameObject MuerteDialogo;
    public TextMeshProUGUI DeathText;
    public LocalizedString[] DialogueLocalize;
    public string[] LocalizeStrings;

    void Start()
    {
        MuerteDialogo.SetActive(false);
    }

    public IEnumerator FadeInMuerte()
    {
        yield return new WaitForSeconds(2f);
        MuerteDialogo.SetActive(true);
        float Alphavalue = 0;

        GameObject child = MuerteDialogo.transform.GetChild(0).gameObject;
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

        yield return new WaitForSeconds(2f);

        string Originalmessage1 = DialogueLocalize[0].GetLocalizedString(LocalizeStrings[0]);
        foreach (var d in Originalmessage1)
        {
            DeathText.text += d;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1f);

        string Originalmessage2 = DialogueLocalize[1].GetLocalizedString(LocalizeStrings[1]);
        foreach (var d in Originalmessage2)
        {
            DeathText.text += d;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Credits");
    }

}
