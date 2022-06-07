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

    //DIALOGO
    public GameObject DialoguePanel;
    public int CurrentDialogueText;
    public string[] Dialogo;
    public string[] CurrentLevel;
    public TextMeshProUGUI DialogueText;
    public bool DialogueAnimDone = false;


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
    }


    #region MenyButtons
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

    #region SoundSettings
    //SLIDER M�SICA
    public void UpdateMusicVolume(float v)
    {
        MainCameraAudioSource.volume = v;
        DataPersistance.DracoState.MusicVolume = v;
        //MenuMusicVolume = DataPersistance.DracoState.MusicVolume;
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
    //TOGGLE M�SICA
    public void UpdateIntMusic_Sound()
    {
        //bool BoolToggleMusic = MusicToggle.GetComponent<Toggle>().isOn;
        intToggleMusic = BoolToInt(MusicToggle.GetComponent<Toggle>().isOn);
        intToggleSound = BoolToInt(SoundToggle.GetComponent<Toggle>().isOn);
    }


    //SLIDER SONIDO
    public void UpdateSoundVolume(float v)
    {
        MenuGameManagerAudioSource.volume = v;
        DataPersistance.DracoState.SoundVolume = v;
        //MenuSoundVolume = DataPersistance.DracoState.SoundVolume;
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
    }
    public void NextButton()
    {
        //Hasta que no se haya acabado de reproducir el di�logo, el jugador no podr� darle a next.
        if (DialogueAnimDone == true)
        {
            CurrentDialogueText++;

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

    }

    private IEnumerator Letters()
    {
        DialogueAnimDone = false;

        string Originalmessage = DialogueText.text;

        DialogueText.text = "";

        foreach (var d in Originalmessage) //var (comod�n)
        {
            DialogueText.text += d;
            yield return new WaitForSeconds(0.02f);
        }

        DialogueAnimDone = true;
    }
}
