using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlow : MonoBehaviour
{
    private PlayerController playerControllerScript;
    private void Start()
    {
        LoadUserOptions();

        playerControllerScript = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name != "MainMenu")
        {
            DataPersistance.Time += Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log($"Llevas jugando {UnityEngine.Mathf.Round(DataPersistance.Time)} segundos, viciado");
        }
    }

    public void OnCollisionEnter(Collision otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Player"))
        {
            DataPersistance.SaveCoins(otherCollider.gameObject.GetComponent<PlayerController>().MoneyCounter); //save user options, al pasarse un nivel se guarda el numero de monedas recolectadas

            DataPersistance.CoinsColected += playerControllerScript.levelCoinCounter;
            DataPersistance.ItemsCollected += playerControllerScript.itemsCounter;
            DataPersistance.KilledEnemies += playerControllerScript.enemyCounter;

            if(playerControllerScript.pacificRoute == 0)
            {
                DataPersistance.PacificRoute = 0;
            }

            if(DataPersistance.CurrentLevel == 1)
            {
                DataPersistance.Level1Done = 1;
            }

            else if(DataPersistance.CurrentLevel == 2)
            {
                DataPersistance.Level2Done = 1;
            }

            else if (DataPersistance.CurrentLevel == 3)
            {
                DataPersistance.Level3Done = 1;
            }

            else if (DataPersistance.CurrentLevel == 4)
            {
                DataPersistance.Level4Done = 1;
            }

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

            //Logros zone
            DataPersistance.CoinsColected = PlayerPrefs.GetInt("Coins_Colected");

            DataPersistance.PacificRoute = PlayerPrefs.GetInt("Pacific_Route");
            DataPersistance.KilledEnemies= PlayerPrefs.GetInt("Killed_Enemies");
            DataPersistance.HasKilledSlums = PlayerPrefs.GetInt("Has_Killed_Slums");

            DataPersistance.Fireballs = PlayerPrefs.GetInt("FireBalls");

            DataPersistance.ItemsCollected = PlayerPrefs.GetInt("Items_Collected");

            DataPersistance.TotalAttack = PlayerPrefs.GetInt("Total_Attack");
            DataPersistance.TotalDefense = PlayerPrefs.GetInt("Total_Defense");
            DataPersistance.TotalBoost = PlayerPrefs.GetInt("Total_Boost");

            DataPersistance.Bullets = PlayerPrefs.GetInt("Bullets_Count");
            DataPersistance.MediumAttack = PlayerPrefs.GetInt("Medium_Attack");

            DataPersistance.DeadInBattle = PlayerPrefs.GetInt("Dead_In_Battle");
            DataPersistance.DeadInBattle = PlayerPrefs.GetInt("Boss_Is_Dead");

            DataPersistance.RobertHasTalk = PlayerPrefs.GetInt("Robert_Has_Talk");
            DataPersistance.RobertHasTalk = PlayerPrefs.GetInt("Robert_Is_Friendly");

            DataPersistance.Time = PlayerPrefs.GetFloat("Time");

            DataPersistance.TutorialDone = PlayerPrefs.GetInt("Tutorial_Done");
            DataPersistance.Level1Done = PlayerPrefs.GetInt("Level1_Done");
            DataPersistance.Level2Done = PlayerPrefs.GetInt("Level2_Done");
            DataPersistance.Level3Done = PlayerPrefs.GetInt("Level3_Done");
            DataPersistance.Level4Done = PlayerPrefs.GetInt("Level4_Done");

        }
    }

    
}
