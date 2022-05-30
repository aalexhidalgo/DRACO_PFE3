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

    //Music
    public float SoundVolume;
    public int SoundToggle;
    public float MusicVolume;
    public int MusicToggle;

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

        //Nivel en el que nos encontramos y store
        PlayerPrefs.SetInt("Current_Level", CurrentLevel);
        PlayerPrefs.SetInt("Store_Done", Storedone);

        //Tienda (Cantidad de stock y valor de las mejoras)
        PlayerPrefs.SetInt("Fireball_Stock", Fireball);
        PlayerPrefs.SetInt("Shield_Stock", Shield);
        PlayerPrefs.SetInt("Fly_Stock", Fly);
        PlayerPrefs.SetFloat("Fireball_Value", FireballValue);
        PlayerPrefs.SetInt("Shield_Value", ShieldValue);
        PlayerPrefs.SetFloat("Fly_Value", FlyValue);

        //Musica (Sonido y Música de fondo)
        PlayerPrefs.SetFloat("Sound_Volume", SoundVolume);
        PlayerPrefs.SetInt("Sound_Toggle", SoundToggle);
        PlayerPrefs.SetFloat("Music_Volume", MusicVolume);
        PlayerPrefs.SetInt("Music_Toggle", MusicToggle);

        /*Meter audiosource en main menu a la cámara (música de fondo) a la cámara de cada nivel
        (música de fondo) y jugador (sonidos) + store (empty betweenlevels sonido
        y cámara música fondo)*/
    }

    public void SaveCoins(int Coins)
    {
        //Contador de monedas
        MoneyCounter = Coins;
    }

   
}
