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
    public bool pause = false;

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
    

    //Ajustes Player
    private AudioSource GameManagerAudioSource;
    private AudioSource MainCameraAudioSource;

    public Slider MusicSlider;
    public Toggle MusicToggle;

    public Slider SoundSlider; 
    public Toggle SoundToggle;

    public Image PauseButton;
    public Sprite UnPause;
    public Sprite Pause;

    //Scripts
    private PlayerController PlayerControllerScript;

    //PostProcesado
    private GameObject PostProcesadoMuerte;


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
        PauseButton.sprite = UnPause;
        DataPersistance.DracoState.SaveForFutureGames();
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
            PauseButton.sprite = Pause;
        }
    }

    void Start()
    {
        MoneyText.text = DataPersistance.DracoState.MoneyCounter.ToString();
        LifeImage = LifeImage.GetComponent<Image>();
        FlybarCounter = Flybar.fillAmount;
        ShieldState = ShieldState.GetComponent<Image>();
        PlayerControllerScript = FindObjectOfType<PlayerController>();

        PostProcesadoMuerte = GameObject.Find("PostProcesado");
        PostProcesadoMuerte.SetActive(false);
        

        MainCameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        MainCameraAudioSource.volume = DataPersistance.DracoState.MusicVolume;
        GameManagerAudioSource = GetComponent<AudioSource>();
        GameManagerAudioSource.volume = DataPersistance.DracoState.SoundVolume;

        UpdateMusicSound_Value();
        UpdateMusicSound_Active();
    }

    void Update()
    {
        if(PlayerControllerScript.GameOver)
        {
            PostProcesadoMuerte.SetActive(true);
        }
    }

    //Conectamos los valores de los sliders al volumen de los AudioSource
    public void UpdateMusicSound_Value()
    {
        MusicSlider.value = MainCameraAudioSource.volume;
        SoundSlider.value = GameManagerAudioSource.volume;

        //DataPersistance.DracoState.MusicVolume = MusicSlider.value;
        //DataPersistance.DracoState.SoundVolume = SoundSlider.value;
    }

    public void UpdateMusicSound_Active()
    {
        if(DataPersistance.DracoState.MusicToggle == 0)
        {
            MusicToggle.isOn = false;
        }
        else
        {
            MusicToggle.isOn = true;
        }

        if (DataPersistance.DracoState.SoundToggle == 0)
        {
            SoundToggle.isOn = false;
        }
        else
        {
            SoundToggle.isOn = true;
        }
    }

    //Guardamos en Data Persistance a tiempo real el valor de los sliders
    public void Music_Sound_Slider()
    {
        DataPersistance.DracoState.MusicVolume = MusicSlider.value;
        DataPersistance.DracoState.SoundVolume = SoundSlider.value;
    }

    //Guardamos en Data Persistance a tiempo real el valor de los toggles
    public void Music_Sound_Toggle()
    {
        if (MusicToggle.isOn == false)
        {
            DataPersistance.DracoState.MusicToggle = 0;
        }
        else
        {
            DataPersistance.DracoState.MusicToggle = 1;
        }

        if (SoundToggle.isOn == false)
        {
            DataPersistance.DracoState.SoundToggle = 0;
        }
        else
        {
            DataPersistance.DracoState.SoundToggle = 1;
        }
    }
}
