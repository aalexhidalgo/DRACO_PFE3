using UnityEngine;

public class DataPersistance
{

    //Variables

    public static int MoneyCounter = 0;
    public static int CurrentLevel = 1;
    public static int Storedone = 1;

    //Store Stock
    public static int Fireball;
    public static int Shield;
    public static int Fly;

    //Store Value
    public static float FireballValue;
    public static int ShieldValue = 1;
    public static float FlyValue;

    //Music
    public static float SoundVolume;
    public static int SoundToggle;

    public static float MusicVolume;
    public static int MusicToggle;

    //Controls
    public static int SwitchControls;

    public static void SaveForFutureGames()
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

        //Controles con los que jugamos
        PlayerPrefs.SetInt("Switch_Controls", SwitchControls);

        /*Meter audiosource en main menu a la cámara (música de fondo) a la cámara de cada nivel
        (música de fondo) y jugador (sonidos) + store (empty betweenlevels sonido
        y cámara música fondo)*/
    }

    public static void SaveCoins(int Coins)
    {
        //Contador de monedas
        MoneyCounter = Coins;
    }

   
}
