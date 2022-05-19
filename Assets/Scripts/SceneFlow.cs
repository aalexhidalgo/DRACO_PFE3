using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlow : MonoBehaviour
{

    private void Start()
    {
        LoadUserOptions();
    }
    public void OnCollisionEnter(Collision otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Player"))
        {
            DataPersistance.DracoState.SaveCoins(otherCollider.gameObject.GetComponent<PlayerController>().MoneyCounter); //save user options, al pasarse un nivel se guarda el numero de monedas recolectadas

            DataPersistance.DracoState.SaveForFutureGames();

            SceneManager.LoadScene("Store");
        }
    }

    public void LoadUserOptions()
    {
        if (PlayerPrefs.HasKey("Money_Counter"))
        {
            DataPersistance.DracoState.MoneyCounter = PlayerPrefs.GetInt("Money_Counter");
        }
    }
}
