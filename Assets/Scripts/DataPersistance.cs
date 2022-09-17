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
    //public static int SwitchControls;

    //Languages
    public static int LanguageIntValue;


    #region Logros Zone
    //Logros
    public static int CoinsColected;
    public static int PacificRoute = 1; //1 es true, recuerda
    public static int KilledEnemies = 0; // 0 es false, recuerdalo
    public static int HasKilledSlums = 0; //No has matado Slums (slimes)
    public static int Fireballs = 0;
    public static int ItemsCollected = 0; //Escudo, corazón y vuelo de todo el juegos recogidos
    public static int TotalAttack; //Si vale 1 agotamos el producto
    public static int TotalDefense;
    public static int TotalBoost;
    public static int Bullets = 0;
    public static int MediumAttack = 0;

    public static int DeadInBattle = 0;
    public static int BossIsDead = 0;

    public static int RobertHasTalk = 0;
    public static int RobertIsFriedly = 0;
    public static float Time = 0;

    public static int TutorialDone = 0;

    public static int Level1Done = 0;
    public static int Level2Done = 0;
    public static int Level3Done = 0;
    public static int Level4Done = 0;
    #endregion

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
        //PlayerPrefs.SetInt("Switch_Controls", SwitchControls);

        //En que idioma hemos configurado el juego
        PlayerPrefs.SetInt("Language_Int", LanguageIntValue);



        #region Logros
        //Logros zone

        //Monedas recolectadas durante la partida, sin restarle las compras
        PlayerPrefs.SetInt("Coins_Colected", CoinsColected); //POSIBLE

        PlayerPrefs.SetInt("Pacific_Route", PacificRoute); //POSIBLE
        PlayerPrefs.SetInt("Killed_Enemies", KilledEnemies); //Easy posible
        PlayerPrefs.SetInt("Has_Killed_Slums", HasKilledSlums);

        PlayerPrefs.SetInt("FireBalls", Fireballs);

        PlayerPrefs.SetInt("Items_Collected", ItemsCollected);

        PlayerPrefs.SetInt("Total_Attack", TotalAttack);
        PlayerPrefs.SetInt("Total_Defense", TotalDefense);
        PlayerPrefs.SetInt("Total_Boost", TotalBoost);

        PlayerPrefs.SetInt("Bullets_Count", Bullets);
        PlayerPrefs.SetInt("Medium_Attack", MediumAttack);

        PlayerPrefs.SetInt("Dead_In_Battle", DeadInBattle);
        PlayerPrefs.SetInt("Boss_Is_Dead", BossIsDead);

        PlayerPrefs.SetInt("Robert_Has_Talk", RobertHasTalk);
        PlayerPrefs.SetInt("Robert_Is_Friendly", RobertIsFriedly);

        PlayerPrefs.SetFloat("Time", Time);

        PlayerPrefs.SetInt("Tutorial_Done", TutorialDone);
        PlayerPrefs.SetInt("Level1_Done", Level1Done);
        PlayerPrefs.SetInt("Level2_Done", Level2Done);
        PlayerPrefs.SetInt("Level3_Done", Level3Done);
        PlayerPrefs.SetInt("Level4_Done", Level4Done);
        #endregion

    }

    public static void SaveCoins(int CurrentCoins)
    {
        //Contador de monedas
        MoneyCounter = CurrentCoins;
        //CoinsColected += CollectedMoney;
    }
}
