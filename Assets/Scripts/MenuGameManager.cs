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
    public float MenuSoundVolume;
    public int SoundToggle;
    public Slider SoundSlider;
    public float MenuMusicVolume;
    public int MusicToggle;
    public Slider MusicSlider;
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
        DataPersistance.DracoState.SaveForFutureGames();
    }

    //OptionsPanel

    void Start()
    {
        MainMenuPanel.SetActive(true);
        OptionsPanel.SetActive(false);
        HowToPlayPanel.SetActive(false);

        //Audiosource
        MenuGameManagerAudioSource = GetComponent<AudioSource>();
        MainCameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();


        //Update
        UpdateGame_Music_Sound();
    }

    //Updeateamos en el start los valores de los sonidos y música (a tiempo real)
    public void UpdateGame_Music_Sound()
    {
        MenuMusicVolume = DataPersistance.DracoState.MusicVolume;
        MusicSlider.value = MenuMusicVolume;
        MainCameraAudioSource.volume = MenuMusicVolume;
        //Meter sonido
    }
    //SLIDER MÚSICA
    public void UpdateMusicVolume()
    {
        MainCameraAudioSource.volume = MusicSlider.value;
        DataPersistance.DracoState.MusicVolume = MusicSlider.value;
        MenuMusicVolume = DataPersistance.DracoState.MusicVolume;
    }

    //TOGGLE MÚSICA
    /*public void UpdateBoolToIntMusic()
    {
        BoolToggleMusic = BackgroundMusicToggle.GetComponent<Toggle>().isOn;

        if (BoolToggleMusic == true)
        {
            IntToggleMusic = 1;
        }
        else
        {
            IntToggleMusic = 0;
        }
    }
    */

    //SLIDER MÚSICA
    public void UpdateSoundVolume()
    {
        MenuGameManagerAudioSource.volume = SoundSlider.value;
        DataPersistance.DracoState.SoundVolume = SoundSlider.value;
        MenuSoundVolume = DataPersistance.DracoState.SoundVolume;
    }

    //TOGGLE SONIDO
}
