using UnityEngine;

public class DataPersistance : MonoBehaviour
{
    //Instancia compartida

    public static DataPersistance DracoState;

    //Variables

    public int MoneyCounter = 0;
    public int CurrentLevel = 0;
    public int Storedone = 1;

    //Store Stock
    public int Fireball;
    public int Shield;
    public int Fly;

    //Store Value
    public float FireballValue;
    public int ShieldValue = 1;
    public float FlyValue;

    void Awake()
    {
        // Si la instancia no existe
        if (DracoState == null)
        {
            // Configuramos la instancia
            DracoState = this;
            // Nos aseguramos de que no sea destruida con el cambio de escena
            DontDestroyOnLoad(DracoState);
        }
        else
        {
            // Como ya existe una instancia, destruimos la copia
            Destroy(this);
        }
    }


    public void SaveForFutureGames()
    {
        //Contador de monedas
        PlayerPrefs.SetInt("Money_Counter", MoneyCounter);
        PlayerPrefs.SetInt("Current_Level", CurrentLevel);
        PlayerPrefs.SetInt("Store_Done", Storedone);
        PlayerPrefs.SetInt("Fireball_Stock", Fireball);
        PlayerPrefs.SetInt("Shield_Stock", Shield);
        PlayerPrefs.SetInt("Fly_Stock", Fly);
        PlayerPrefs.SetFloat("Fireball_Value", FireballValue);
        PlayerPrefs.SetInt("Shield_Value", ShieldValue);
        PlayerPrefs.SetFloat("Fly_Value", FlyValue);
    }

    public void SaveCoins(int Coins)
    {
        //Contador de monedas
        MoneyCounter = Coins;
    }

   
}
