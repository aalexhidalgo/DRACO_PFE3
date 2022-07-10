using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlow : MonoBehaviour
{
    private MenuGameManager MenuGameManagerScript;
    //private GameManager GameManagerScript;
    private void Start()
    {
        //MenuGameManagerScript = GameObject.Find("MenuGameManager").GetComponent<MenuGameManager>();
        //GameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        LoadUserOptions();
    }

    public void OnCollisionEnter(Collision otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Player"))
        {
            DataPersistance.SaveCoins(otherCollider.gameObject.GetComponent<PlayerController>().MoneyCounter); //save user options, al pasarse un nivel se guarda el numero de monedas recolectadas
            DataPersistance.CurrentLevel++;

            DataPersistance.SaveForFutureGames();

            SceneManager.LoadScene("Store");
        }
    }

    public void LoadUserOptions()
    {
        if (PlayerPrefs.HasKey("Fireball_Stock"))
        {
            DataPersistance.MoneyCounter = PlayerPrefs.GetInt("Money_Counter");
            DataPersistance.CurrentLevel = PlayerPrefs.GetInt("Current_Level");
            DataPersistance.Storedone = PlayerPrefs.GetInt("Store_Done");
            DataPersistance.Fireball = PlayerPrefs.GetInt("Fireball_Stock");
            DataPersistance.Shield = PlayerPrefs.GetInt("Shield_Stock");
            DataPersistance.Fly = PlayerPrefs.GetInt("Fly_Stock");
            DataPersistance.FireballValue = PlayerPrefs.GetFloat("Fireball_Value");
            DataPersistance.ShieldValue = PlayerPrefs.GetInt("Shield_Value");
            DataPersistance.FlyValue = PlayerPrefs.GetFloat("Fly_Value");
            DataPersistance.MusicVolume = PlayerPrefs.GetFloat("Music_Volume");
            DataPersistance.SoundVolume = PlayerPrefs.GetFloat("Sound_Volume");
            DataPersistance.MusicToggle = PlayerPrefs.GetInt("Music_Toggle");
            DataPersistance.SoundToggle = PlayerPrefs.GetInt("Sound_Toggle");

            /*
            MenuGameManagerScript.MenuMusicVolume = PlayerPrefs.GetFloat("Music_Volume");
            MenuGameManagerScript.MusicSlider.value = MenuGameManagerScript.MenuMusicVolume;
            MenuGameManagerScript.MenuSoundVolume = PlayerPrefs.GetFloat("Sound_Volume");
            MenuGameManagerScript.SoundSlider.value = MenuGameManagerScript.MenuSoundVolume;
            
            if(PlayerPrefs.GetInt("Music_Toggle") == 0)
            {
                MenuGameManagerScript.MusicToggle.isOn = false;
            }
            else if(PlayerPrefs.GetInt("Music_Toggle") == 1)
            {
                MenuGameManagerScript.MusicToggle.isOn = true;
            }
            
            if(PlayerPrefs.GetInt("Sound_Toggle") == 0)
            {
                MenuGameManagerScript.SoundToggle.isOn = false;
            }
            else if(PlayerPrefs.GetInt("Sound_Toggle") == 1)
            {
                MenuGameManagerScript.SoundToggle.isOn = true;
            }
            */
        }
    }

    
}
