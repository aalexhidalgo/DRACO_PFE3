using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BetweenLevelsManager : MonoBehaviour
{
    public GameObject LevelPanel;
    public GameObject StorePanel;

    //Dialogo
    public GameObject DialogueImage;
    public TextMeshProUGUI DialogueText;
    public GameObject Next;
    public GameObject Yes;
    public GameObject No;
    public string[] DialogueArray;
    public int CurrentDialogueText;

    void Start()
    {
        
    }

    public void Level_1()
    {
        Debug.Log("Level 1");
        SceneManager.LoadScene("Level_1");
    }

    public void Level_2()
    {
        Debug.Log("Level 2");
        SceneManager.LoadScene("Level_2");
    }

    public void Level_3()
    {
        Debug.Log("Level 3");
        SceneManager.LoadScene("Level_3");
    }

    public void Level_4()
    {
        Debug.Log("Level 4");
        SceneManager.LoadScene("Level_4");
    }

    public void Level_Boss()
    {
        Debug.Log("Level Boss");
        SceneManager.LoadScene("Level_Boss");
    }

    public void ContinueButton()
    {
        LevelPanel.SetActive(true);
        StorePanel.SetActive(false);
    }

    public void NextButton()
    {
        CurrentDialogueText++;
        DialogueText.text = DialogueArray[CurrentDialogueText];

        //Esta parte no funciona
        if(CurrentDialogueText > DialogueArray.Length)
        {
            Debug.Log("Funciono");
            DialogueImage.SetActive(false);
        }
    }

    public void YesButton()
    {
        //Restar monedas(PlayerController), recoger item, instanciar partículas y minimizar alpha imagen a través de animación???
    }

    public void NoButton()
    {
        DialogueImage.SetActive(false);
    }

    public void AttackStat_1()
    {
        DialogueImage.SetActive(true);
        DialogueText.text = "MMM, BUENA ELECCION...! INCREMENTA EL ATAQUE BASICO EN UN 25%. TE LO PUEDES LLEVAR POR EL PRECIO DE 75€. DESEAS COMPRAR?";
        Next.SetActive(false);
        Yes.SetActive(true);
        No.SetActive(true);
    }

    public void DefenseStat_2()
    {
        DialogueImage.SetActive(true);
        DialogueText.text = "GRAN DEFENSA! INCREMENTA LA DEFENSA BASICA EN UN 25%. TE LO PUEDES LLEVAR POR EL PRECIO DE 50€. DESEAS COMPRAR?";
        Next.SetActive(false);
        Yes.SetActive(true);
        No.SetActive(true);
    }
    public void BoostStat_3()
    {
        DialogueImage.SetActive(true);
        DialogueText.text = "HASTA EL INFINITO Y MAS ALLA! INCREMENTA LA CAPACIDAD DE VUELO EN UN 25%. TE LO PUEDES LLEVAR POR EL PRECIO DE 100€. DESEAS COMPRAR?";
        Next.SetActive(false);
        Yes.SetActive(true);
        No.SetActive(true);
    }
}
