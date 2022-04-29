using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Paneles
    public GameObject PauseMenuPanel;
    private bool pause = false;

    //Contadores y movidas de los propsç
    public Image LifeImage;
    public Sprite[] LifeSprites;

    //PauseMenuPanel
    /*
    public void RestartButton()
    {
        ReloadScene.blablabla
    }

    //No funciona, averiguar
    public void ResumeButton()
    {
        PauseMenuPanel.SetActive(false);
    }

    public void ExitButton()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
    */

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
        LifeImage = LifeImage.GetComponent<Image>();
    }
}
