using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Paneles
    public GameObject PauseMenuPanel;
    private bool pause = false;

    //Contadores y movidas de los props
    public Image LifeImage;
    public Sprite[] LifeSprites;
    public GameObject ShieldImage;
    public Sprite[] ShieldSprites;
    public Image ShieldState;
    public Image Flybar;
    public float FlybarCounter;

    public TextMeshProUGUI MoneyText;
    private int NumberOfCoins;
    private PlayerController PlayerControllerScript;

    //Scripts
    


    //PauseMenuPanel
    
    //Reinciamos el nivel en el que nos encontramos
    public void RestartButton()
    {
        SceneManager.LoadScene(DataPersistance.DracoState.CurrentLevel);
    }
    
    //Descansito
    public void ResumeButton()
    {
        PauseMenuPanel.SetActive(false);
        pause = false;
    }
    
    //Nos vamos del juego
    public void ExitButton()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void PauseMenuButton()
    {
        if(pause == false)
        {
            PauseMenuPanel.SetActive(true);
            pause = true;
        }
        else
        {
            PauseMenuPanel.SetActive(false);
            pause = false;
        }
    }
    void Start()
    {
        MoneyText.text = DataPersistance.DracoState.MoneyCounter.ToString();
        LifeImage = LifeImage.GetComponent<Image>();
        FlybarCounter = Flybar.fillAmount;
        ShieldState = ShieldState.GetComponent<Image>();
    }


}
