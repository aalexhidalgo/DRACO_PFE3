using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Variables
    private AudioSource GameManagerAudioSource;
    private AudioSource MainCameraAudioSource;
    //Paneles

    public GameObject MainMenuPanel;
    public GameObject OptionsPanel;
    public GameObject HowToPlayPanel;
    public GameObject PauseMenuPanel;

    //Panel principal

    public void StartButton()
    {
        Debug.Log("Start");
        SceneManager.LoadScene("Game");
    }
    public void HowToPlayButton()
    {
        MainMenuPanel.SetActive(false);
        HowToPlayPanel.SetActive(true);
    }
    public void OptionsButton()
    {
        MainMenuPanel.SetActive(false);
        OptionsPanel.SetActive(true);
    }
    public void ExitButton()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    //Volver hacia atrás

    public void ReturnButton()
    {
        OptionsPanel.SetActive(false);
        HowToPlayPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    //OptionsPanel

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
        PauseMenuPanel.SetActive(true);
    }

    void Start()
    {
        MainMenuPanel.SetActive(true);
        OptionsPanel.SetActive(false);
        HowToPlayPanel.SetActive(false);

        //Audiosource
        GameManagerAudioSource = GetComponent<AudioSource>();
        MainCameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

}
