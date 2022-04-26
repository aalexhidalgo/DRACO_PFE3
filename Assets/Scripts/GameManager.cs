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
        ReloadScene
    }

    public void ResumeButton()
    {
        Desactivar panel de pausa y volver al juego
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
