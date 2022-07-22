using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class MenuGameManager : MonoBehaviour
{
    //Botones
    public Button nextButton;
    public Button returnButton;
    public Button[] switchControlButton;
    public Button startButton;


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
    public GameObject KeyboardPanel;
    public GameObject GamepadPanel;

    //Panel principal

    //DIALOGO
    public GameObject DialoguePanel;
    public int CurrentDialogueText;
    public string[] Dialogo;
    public TextMeshProUGUI DialogueText;
    public bool DialogueAnimDone = false;
    public GameObject Next;

    //UI GAMEPAD
    public GameObject SquareButton;
    public Toggle Controller;
    public GameObject MenuPanel;
    public GameObject OptionPanel;
    public GameObject ControlPanel;
    public Button controlReturnButton;

    //Scripts
    private GamePadController GamePadControllerScript;

    void Start()
    {
        MainMenuPanel.SetActive(true);
        OptionsPanel.SetActive(false);
        HowToPlayPanel.SetActive(false);

        DialogueText.text = Dialogo[CurrentDialogueText];
        //Audiosource
        MenuGameManagerAudioSource = GetComponent<AudioSource>();
        MainCameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        MenuGameManagerAudioSource.Stop();
        LoadMusicSoundValue();
        LoadGameControls();

        GamePadControllerScript = FindObjectOfType<GamePadController>();       
    }


    #region MenuButtons
    public void StartButton()
    {
        DataPersistance.MoneyCounter = 0;
        DataPersistance.Storedone = 1;
        DataPersistance.CurrentLevel = 1;
        DataPersistance.Fireball = 4;
        DataPersistance.Shield = 3;
        DataPersistance.Fly = 2;
        DataPersistance.FireballValue = 1f;
        DataPersistance.ShieldValue = 0;
        DataPersistance.FlyValue = 0.5f;
        DataPersistance.SaveForFutureGames();
        SceneManager.LoadScene(DataPersistance.CurrentLevel);

    }
    public void ContinueButton()
    {
        if (DataPersistance.Storedone == 1)
        {
            SceneManager.LoadScene(DataPersistance.CurrentLevel);
        }
        else
        {
            SceneManager.LoadScene("Store");
        }
    }
    public void HowToPlayButton()
    {
        switchControlButton[DataPersistance.SwitchControls].Select();
        MainMenuPanel.SetActive(false);
        HowToPlayPanel.SetActive(true);

        if(DataPersistance.SwitchControls == 0)
        {
            GamepadPanel.SetActive(false);
            KeyboardPanel.SetActive(true);
        }
        else
        {
            KeyboardPanel.SetActive(false);
            GamepadPanel.SetActive(true);
        }
    }
    public void OptionsButton()
    {
        returnButton.Select();
        MainMenuPanel.SetActive(false);
        OptionsPanel.SetActive(true);
    }
    public void ExitButton()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void ReturnButton()
    {

        if(GamePadControllerScript.PS4_Controller == 1)
        {
            
            startButton.Select();
        }
        OptionsPanel.SetActive(false);
        HowToPlayPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
        DataPersistance.MusicToggle = intToggleMusic;
        DataPersistance.SoundToggle = intToggleSound;
        DataPersistance.SaveForFutureGames();
    }

    //Mostramos el tipo de controles dependiendo de si se juega con teclado o mando
    public void KeyboardControls()
    {
        GamepadPanel.SetActive(false);
        KeyboardPanel.SetActive(true);
        DataPersistance.SwitchControls = 0;
    }
    public void GamePadControls()
    {
        KeyboardPanel.SetActive(false);
        GamepadPanel.SetActive(true);
        DataPersistance.SwitchControls = 1;
    }

    public void LoadGameControls()
    {
        if (PlayerPrefs.HasKey("Switch_Controls"))
        {
            DataPersistance.SwitchControls = PlayerPrefs.GetInt("Switch_Controls");
        }
    }
    #endregion
    //OptionsPanel

    #region MusicSoundSettings
    //SLIDER MÚSICA
    public void UpdateMusicVolume(float v)
    {
        MainCameraAudioSource.volume = v;
        DataPersistance.MusicVolume = v;
       
    }

    public void LoadMusicSoundValue()
    {
        if (PlayerPrefs.HasKey("Music_Volume"))
        {
            MusicSlider.value = PlayerPrefs.GetFloat("Music_Volume");
            SoundSlider.value = PlayerPrefs.GetFloat("Sound_Volume");
            MusicToggle.isOn = IntToBool(PlayerPrefs.GetInt("Music_Toggle"));
            SoundToggle.isOn = IntToBool(PlayerPrefs.GetInt("Sound_Toggle"));

        }      
    }
    //TOGGLE MÚSICA
    public void UpdateIntMusic_Sound()
    {        
        intToggleMusic = BoolToInt(MusicToggle.GetComponent<Toggle>().isOn);
        intToggleSound = BoolToInt(SoundToggle.GetComponent<Toggle>().isOn);
    }


    //SLIDER SONIDO
    public void UpdateSoundVolume(float v)
    {
        MenuGameManagerAudioSource.volume = v;
        DataPersistance.SoundVolume = v;
    }

    public int BoolToInt(bool b)
    {
        return b ? 1 : 0;
    }

   public bool IntToBool(int i)
    {
        return !(i == 0);
    }
    #endregion


    public void ShowPanels()
    {
        DialoguePanel.SetActive(true);
        StartCoroutine(Letters());
        EventSystem.current.SetSelectedGameObject(null);
    }
    public void NextButton()
    {
        //Hasta que no se haya acabado de reproducir el diálogo, el jugador no podrá darle a next.
        if (DialogueAnimDone == true)
        {
            CurrentDialogueText++;
            Next.SetActive(false);
            if (CurrentDialogueText >= Dialogo.Length)
            {
                StartButton();
            }
            else
            {
                DialogueText.text = Dialogo[CurrentDialogueText];
                StartCoroutine(Letters());
            }
        }
        else
        {
           
        }

    }

    private IEnumerator Letters()
    {
        DialogueAnimDone = false;

        string Originalmessage = DialogueText.text;

        DialogueText.text = "";

        foreach (var d in Originalmessage) //var (comodín)
        {
            DialogueText.text += d;
            yield return new WaitForSeconds(0.02f);
        }
       
        Next.SetActive(true);
        nextButton.Select();
        DialogueAnimDone = true;
    }

    void Update()
    {
        if (Input.GetButtonDown("Press Me")) //Mediante ratón o Joystick button 0
        {
            MenuGameManagerAudioSource.Play();
        }

        //Trampita: Saltarse el diálogo inicial Mediante X en teclado y Joystick button 3 en mando (Triángulo)
        if(Input.GetButtonDown("Tramposo"))
        {
            Debug.Log("De oca a oca y tiro porque me toca");
            StartButton();
        }

        if(GamePadControllerScript.PS4_Controller == 1)
        {
            SquareButton.SetActive(true);
            Controller.isOn = true;
        }
        else
        {
            SquareButton.SetActive(false);
            Controller.isOn = false;
        }

    }

    //Mando: Toggle
    public void AutoSelectButton()
    {
        if (Controller.isOn == true)
        {
            if (MenuPanel.activeInHierarchy)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(startButton.gameObject);
            }
            else if (OptionPanel.activeInHierarchy)
            {                
                EventSystem.current.SetSelectedGameObject(returnButton.gameObject);
            }
            else if (ControlPanel.activeInHierarchy)
            {
                EventSystem.current.SetSelectedGameObject(controlReturnButton.gameObject);
            }            
            else if (DialoguePanel.activeInHierarchy && Next.activeInHierarchy && DialogueAnimDone == true)
            {
                EventSystem.current.SetSelectedGameObject(Next);
            }

        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
