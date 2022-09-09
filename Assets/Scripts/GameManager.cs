using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    //La navegación entre botones con mando:
    //Cruzeta: 7th Axis y 8th Axis
    //Joystick izquierdo: X Axis y Y axis
    //Joystick derecho: 3rd Axis y 6th Axis

    //Navegación entre botones con Teclado:
    //Ratón
    //Flechas: UpArrow, DownArrow, LeftArrow y RightArrow
    //WASD

    //Botones
    public Button resumeButton; //PAUSA
    public Button restartButton; //GAMEOVER

    #region Pausa
    public GameObject PauseMenuPanel;
    public bool pause = false;
    public Image PauseButton;
    public Sprite UnPause;
    public Sprite Pause;
    #endregion

    //Contadores: Vida, Escudo, Vuelo y Money
    #region Contadores Draco
    //Vida
    public Image LifeImage;
    public Sprite[] LifeSprites;
    //Escudo
    public GameObject ShieldImage;
    public Sprite[] ShieldSprites;
    public Image ShieldState;
    //Vuelo
    public Image Flybar;
    public float FlybarCounter;
    //Money
    public TextMeshProUGUI MoneyText;
    #endregion


    #region Musica y Sonido
    //Ajustes Player
    private AudioSource GameManagerAudioSource;
    private AudioSource MainCameraAudioSource;

    public Slider MusicSlider;
    public Toggle MusicToggle;

    public Slider SoundSlider; 
    public Toggle SoundToggle;

    #endregion

    

    //Scripts
    private SpawnManager SpawnManagerScript;
    private GamePadController GamePadControllerScript;


    //PostProcesado
    public GameObject GameOverPanel;
    private GameObject PostProcesadoMuerte;
    public bool GameOver = false;

    //UI GAMEPAD
    public Toggle Controller;


    #region Pause Panel
    //Reinciamos el nivel en el que nos encontramos
    public void RestartButton()
    {
        GameOver = false;
        DataPersistance.SaveForFutureGames();
        SceneManager.LoadScene(DataPersistance.CurrentLevel);
        Time.timeScale = 1;       
    }

    public void KilledByBoss()
    {
        DataPersistance.DeadInBattle = 1;
    }
    
    //Descansito
    public void ResumeButton()
    {
        restartButton.Select();
        PauseMenuPanel.SetActive(false);
        pause = false;
        PauseButton.sprite = UnPause;
        DataPersistance.SaveForFutureGames();
        if (FindObjectOfType<PlayerController>() != null)
        {
            GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
        }
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
        GameOver = false;
        Debug.Log("Exit");
        DataPersistance.SaveForFutureGames();
        //Application.Quit();
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");

    }

    public void PauseMenuButton()
    {
        if(pause == false)
        {
            if (GamePadControllerScript.PS4_Controller == 1)
            {
                resumeButton.Select();
            }
            PauseMenuPanel.SetActive(true);
            Debug.Log("Me pauso");
            pause = true;
            PauseButton.sprite = Pause;
            Time.timeScale = 0;

            if(FindObjectOfType<PlayerController>() != null)
            {
                GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
            }
        }
        else
        {
            Debug.Log("Me despauso");
            ResumeButton();
        }
    }

    #endregion
    void Start()
    {
        MoneyText.text = DataPersistance.MoneyCounter.ToString();

        LifeImage = LifeImage.GetComponent<Image>();

        FlybarCounter = Flybar.fillAmount;

        ShieldState = ShieldState.GetComponent<Image>();

        SpawnManagerScript = FindObjectOfType<SpawnManager>();
        GamePadControllerScript = FindObjectOfType<GamePadController>();

        PostProcesadoMuerte = GameObject.Find("PostProcesado");
        PostProcesadoMuerte.SetActive(false);

        #region AudioSources
        MainCameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        //MainCameraAudioSource.volume = DataPersistance.MusicVolume;
        GameManagerAudioSource = GetComponent<AudioSource>();
        GameManagerAudioSource.volume = DataPersistance.SoundVolume;

        //UpdateMusicSound_Value();
        UpdateMusicSound_Active();
        restartButton.Select();
        #endregion
    }

    void Update()
    {
        if(GameOver == true)
        {
            PostProcesadoMuerte.SetActive(true);
            GameOverPanel.SetActive(true);
            Time.timeScale = 0;

        }

        //Pausa: Mediante Ratón o Joystick button 9 (Options)
        if (Input.GetButtonDown("Pausa"))
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


        if (GamePadControllerScript.PS4_Controller == 1)
        {
            Controller.isOn = true;
        }
        else
        {
            Controller.isOn = false;
        }
    }

    //Conectamos los valores de los sliders al volumen de los AudioSource
    #region Musica y Sonido
    /*public void UpdateMusicSound_Value()
    {
        MusicSlider.value = MainCameraAudioSource.volume;
        SoundSlider.value = GameManagerAudioSource.volume;
    }
    */
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
    #endregion

    //Mando: Toggle
    public void AutoSelectButton()
    {
        if (Controller.isOn == true)
        {
            if (PauseMenuPanel.activeInHierarchy)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);
            }
            else if (GameOverPanel.activeInHierarchy)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(restartButton.gameObject);
            }

        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

}
