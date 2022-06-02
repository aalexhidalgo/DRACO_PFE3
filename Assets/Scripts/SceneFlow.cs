using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlow : MonoBehaviour
{
    private MenuGameManager MenuGameManagerScript;
    private void Start()
    {
        MenuGameManagerScript = GameObject.Find("MenuGameManager").GetComponent<MenuGameManager>();
        LoadUserOptions();
    }

    public void OnCollisionEnter(Collision otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Player"))
        {
            DataPersistance.DracoState.SaveCoins(otherCollider.gameObject.GetComponent<PlayerController>().MoneyCounter); //save user options, al pasarse un nivel se guarda el numero de monedas recolectadas
            DataPersistance.DracoState.CurrentLevel++;
            DataPersistance.DracoState.SaveForFutureGames();

            SceneManager.LoadScene("Store");
        }
    }

    public void LoadUserOptions()
    {
        if (PlayerPrefs.HasKey("Fireball_Stock"))
        {
            DataPersistance.DracoState.MoneyCounter = PlayerPrefs.GetInt("Money_Counter");
            DataPersistance.DracoState.CurrentLevel = PlayerPrefs.GetInt("Current_Level");
            DataPersistance.DracoState.Storedone = PlayerPrefs.GetInt("Store_Done");
            DataPersistance.DracoState.Fireball = PlayerPrefs.GetInt("Fireball_Stock");
            DataPersistance.DracoState.Shield = PlayerPrefs.GetInt("Shield_Stock");
            DataPersistance.DracoState.Fly = PlayerPrefs.GetInt("Fly_Stock");
            DataPersistance.DracoState.FireballValue = PlayerPrefs.GetFloat("Fireball_Value");
            DataPersistance.DracoState.ShieldValue = PlayerPrefs.GetInt("Shield_Value");
            DataPersistance.DracoState.FlyValue = PlayerPrefs.GetFloat("Fly_Value");
            DataPersistance.DracoState.MusicVolume = PlayerPrefs.GetFloat("Music_Volume");
            DataPersistance.DracoState.SoundVolume = PlayerPrefs.GetFloat("Sound_Volume");

            MenuGameManagerScript.MenuMusicVolume = PlayerPrefs.GetFloat("Music_Volume");
        }
    }

    
}
