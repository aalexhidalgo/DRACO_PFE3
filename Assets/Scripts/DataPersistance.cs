using UnityEngine;

public class DataPersistance
{

    //Variables

    public static int MoneyCounter = 0;
    public static int CurrentLevel = 0;
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

    //Languages
    public static int LanguageIntValue;

    //Logros
    public static int CoinsColected;
    public static int PacificRoute = 1; //1 es true, recuerda
    public static int GenocideRoute = 0; // 0 es false, recuerdalo


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

        //En que idioma hemos configurado el juego
        PlayerPrefs.SetInt("Language_Int", LanguageIntValue);

        //Monedas recolectadas durante la partida, sin restarle las compras
        PlayerPrefs.SetInt("Coins_Colected", CoinsColected);

        PlayerPrefs.SetInt("Pacific_Route", PacificRoute);
        PlayerPrefs.SetInt("Genocide_Route", GenocideRoute);
        /*Meter audiosource en main menu a la cámara (música de fondo) a la cámara de cada nivel
        (música de fondo) y jugador (sonidos) + store (empty betweenlevels sonido
        y cámara música fondo)*/
    }

    public static void SaveCoins(int CurrentCoins, int CollectedMoney)
    {
        //Contador de monedas
        MoneyCounter = CurrentCoins;
        CoinsColected += CollectedMoney;
    }
}
