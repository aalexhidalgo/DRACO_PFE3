using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuGameManager : MonoBehaviour
{
    //Variables
    private AudioSource MenuGameManagerAudioSource;
    private AudioSource MainCameraAudioSource;

    //Musica

    public Toggle SoundToggle;
    public int intToggleSound;

    public Slider SoundSlider;
    public float MenuSoundVolume;

    public Toggle MusicToggle;
    public int intToggleMusic;

    public Slider MusicSlider;
    public float MenuMusicVolume;
    //Paneles

    public GameObject MainMenuPanel;
    public GameObject OptionsPanel;
    public GameObject HowToPlayPanel;

    //Panel principal

    public string[] CurrentLevel;

    public void StartButton()
    {
        DataPersistance.DracoState.MoneyCounter = 0;
        DataPersistance.DracoState.Storedone = 1;
        DataPersistance.DracoState.CurrentLevel = 0;
        DataPersistance.DracoState.Fireball = 4;
        DataPersistance.DracoState.Shield = 3;
        DataPersistance.DracoState.Fly = 2;
        DataPersistance.DracoState.FireballValue = 1f;
        DataPersistance.DracoState.ShieldValue = 0;
        DataPersistance.DracoState.FlyValue = 0.5f;
        DataPersistance.DracoState.SaveForFutureGames();
        SceneManager.LoadScene(CurrentLevel[DataPersistance.DracoState.CurrentLevel]);
    }
    public void ContinueButton()
    {
        if (DataPersistance.DracoState.Storedone == 1)
        {
            SceneManager.LoadScene(CurrentLevel[DataPersistance.DracoState.CurrentLevel]);
        }
        else
        {
            SceneManager.LoadScene("Store");
        }
    }

    #region MenuButtons
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

    //Volver hacia atr�s

    public void ReturnButton()
    {
        OptionsPanel.SetActive(false);
        HowToPlayPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
        DataPersistance.DracoState.MusicToggle = intToggleMusic;
        DataPersistance.DracoState.SoundToggle = intToggleSound;
        DataPersistance.DracoState.SaveForFutureGames();
    }
    #endregion
    //OptionsPanel

    void Start()
    {
        MainMenuPanel.SetActive(true);
        OptionsPanel.SetActive(false);
        HowToPlayPanel.SetActive(false);

        //Audiosource
        MenuGameManagerAudioSource = GetComponent<AudioSource>();
        MainCameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        MenuGameManagerAudioSource.Stop();
       
    }

    //SLIDER M�SICA
    public void UpdateMusicVolume()
    {
        MainCameraAudioSource.volume = MusicSlider.value;
        DataPersistance.DracoState.MusicVolume = MusicSlider.value;
        MenuMusicVolume = DataPersistance.DracoState.MusicVolume;
    }

    //TOGGLE M�SICA
    public void UpdateIntMusic_Sound()
    {
        //bool BoolToggleMusic = MusicToggle.GetComponent<Toggle>().isOn;

        if (MusicToggle.GetComponent<Toggle>().isOn == true)
        {
            intToggleMusic = 1;
        }
        else
        {
            intToggleMusic = 0;
        }

        if (SoundToggle.GetComponent<Toggle>().isOn == true)
        {
            intToggleSound = 1;
        }
        else
        {
            intToggleSound = 0;
        }

    }


    //SLIDER SONIDO
    public void UpdateSoundVolume()
    {
        MenuGameManagerAudioSource.volume = SoundSlider.value;
        DataPersistance.DracoState.SoundVolume = SoundSlider.value;
        MenuSoundVolume = DataPersistance.DracoState.SoundVolume;
    }

    //TOGGLE SONIDO
   
}
