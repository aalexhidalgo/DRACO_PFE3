using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    //Botones
    public Button resumeButton; //PAUSA
    public Button restartButton; //GAMEOVER

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
    private SpawnManager SpawnManagerScript;
    private AutoDestroy AutoDestroyScript;

    //PostProcesado
    public GameObject GameOverPanel;
    private GameObject PostProcesadoMuerte;
    public bool GameOver = false;

    //PauseMenuPanel

    //Reinciamos el nivel en el que nos encontramos
    public void RestartButton()
    {
        DataPersistance.SaveForFutureGames();
        SceneManager.LoadScene(DataPersistance.CurrentLevel);
        Time.timeScale = 1;
    }
    
    //Descansito
    public void ResumeButton()
    {
        restartButton.Select();
        PauseMenuPanel.SetActive(false);
        pause = false;
        PauseButton.sprite = UnPause;
        DataPersistance.SaveForFutureGames();
        Time.timeScale = 1;
    }

    //Iniciamos corrutinas Boss
    public void BossResumeButton()
    {
        StartCoroutine(SpawnManagerScript.SpawnRandomPrefab());
        Time.timeScale = 1;
    }
    
    //Nos vamos del juego
    public void ExitButton()
    {
        Debug.Log("Exit");
        DataPersistance.SaveForFutureGames();
        Application.Quit();
    }

    public void PauseMenuButton()
    {
        if(pause == false)
        {
            resumeButton.Select();
            PauseMenuPanel.SetActive(true);
            Debug.Log("Me pauso");
            pause = true;
            PauseButton.sprite = Pause;
            Time.timeScale = 0;
        }
        else
        {
            Debug.Log("Me despauso");
            ResumeButton();
        }
    }

    //Borrar luego
    public void BossPauseMenuButton()
    {
        Time.timeScale = 0;
    }

    void Start()
    {
        MoneyText.text = DataPersistance.MoneyCounter.ToString();
        LifeImage = LifeImage.GetComponent<Image>();
        FlybarCounter = Flybar.fillAmount;
        ShieldState = ShieldState.GetComponent<Image>();
        PlayerControllerScript = FindObjectOfType<PlayerController>();
        SpawnManagerScript = FindObjectOfType<SpawnManager>();
        AutoDestroyScript = FindObjectOfType<AutoDestroy>();

        PostProcesadoMuerte = GameObject.Find("PostProcesado");
        PostProcesadoMuerte.SetActive(false);


        MainCameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        MainCameraAudioSource.volume = DataPersistance.MusicVolume;
        GameManagerAudioSource = GetComponent<AudioSource>();
        GameManagerAudioSource.volume = DataPersistance.SoundVolume;

        UpdateMusicSound_Value();
        UpdateMusicSound_Active();

        restartButton.Select();

    }

    void Update()
    {
        if(GameOver == true)
        {
            PostProcesadoMuerte.SetActive(true);
            GameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }

        //Ratón o Options
        if(Input.GetButtonDown("Pausa"))
        {

            if (pause == true)
            {
                ResumeButton();
            }
            else
            {
                PauseMenuButton();
            }
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
        if(DataPersistance.MusicToggle == 0)
        {
            MusicToggle.isOn = false;
        }
        else
        {
            MusicToggle.isOn = true;
        }

        if (DataPersistance.SoundToggle == 0)
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
        DataPersistance.MusicVolume = MusicSlider.value;
        DataPersistance.SoundVolume = SoundSlider.value;
    }

    //Guardamos en Data Persistance a tiempo real el valor de los toggles
    public void Music_Sound_Toggle()
    {
        if (MusicToggle.isOn == false)
        {
            DataPersistance.MusicToggle = 0;
        }
        else
        {
            DataPersistance.MusicToggle = 1;
        }

        if (SoundToggle.isOn == false)
        {
            DataPersistance.SoundToggle = 0;
        }
        else
        {
            DataPersistance.SoundToggle = 1;
        }
    }
}
