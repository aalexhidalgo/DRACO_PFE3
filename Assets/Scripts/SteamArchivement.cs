using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using UnityEngine.SceneManagement;

public class SteamArchivement
{
   static void Update()
    {
        if (!SteamManager.Initialized) { return; }

        if(DataPersistance.TutorialDone == 1) //Completa el tutorial
        {
            SteamUserStats.SetAchievement("FIRSTS_STEPS");
            SteamUserStats.StoreStats();
        }

        if (DataPersistance.RobertHasTalk == 1) //Habla con Robert
        {
            SteamUserStats.SetAchievement("TALK_TO_ROBERT");
            SteamUserStats.StoreStats();
        }

        if (DataPersistance.Level1Done == 1) //completa el nivel 1
        {
            SteamUserStats.SetAchievement("COMPLETE_LEVEL1");
            SteamUserStats.StoreStats();
        }

        if (DataPersistance.Level2Done == 1) //completa el nivel 2
        {
            SteamUserStats.SetAchievement("COMPLETE_LEVEL2");
            SteamUserStats.StoreStats();
        }

        if (DataPersistance.Level3Done == 1) //completa el nivel 3
        {
            SteamUserStats.SetAchievement("COMPLETE_LEVEL3");
            SteamUserStats.StoreStats();
        }

        if (DataPersistance.Level4Done == 1) //completa el nivel 4
        {
            SteamUserStats.SetAchievement("COMPLETE_LEVEL4");
            SteamUserStats.StoreStats();
        }

        if(DataPersistance.BossIsDead == 1) //Derrota al Boss
        {
            SteamUserStats.SetAchievement("DEFEAT_KING");
            SteamUserStats.StoreStats();
        }

        if (DataPersistance.DeadInBattle == 0 && DataPersistance.BossIsDead == 1) //Mata al Boss en el primer intento
        {
            SteamUserStats.SetAchievement("BOSS_IN_FIRST_TRY");
            SteamUserStats.StoreStats();
        }

        if (DataPersistance.HasKilledSlums == 0 && SceneManager.GetActiveScene().name == "Credits") //No mates a los Slums
        {
            SteamUserStats.SetAchievement("DONT_KILL_SLUMS");
            SteamUserStats.StoreStats();
        }

        if(DataPersistance.KilledEnemies == 0 && SceneManager.GetActiveScene().name == "Credits") //No matar a ningun enemigo
        {
            SteamUserStats.SetAchievement("PACIFIC_ROUTE");
            SteamUserStats.StoreStats();
        }

        if (DataPersistance.KilledEnemies == 52) //No matar a ningun enemigo
        {
            SteamUserStats.SetAchievement("GENOCIDE_ROUTE");
            SteamUserStats.StoreStats();
        }

        if (DataPersistance.CoinsColected == 0 && SceneManager.GetActiveScene().name == "Credits") //No consigas ninguna moneda
        {
            SteamUserStats.SetAchievement("NO_COINS_ROUTE");
            SteamUserStats.StoreStats();
        }

        if (DataPersistance.CoinsColected == 670) //695 si contamos los 5 corazones del mapa que dan 10 monedas cada uno pero consigue todas las monedas
        {
            SteamUserStats.SetAchievement("ALL_COINS_ROUTE");
            SteamUserStats.StoreStats();
        }

        if(DataPersistance.Fireballs == 150) //Dispara 150 bolas de fuego
        {
            SteamUserStats.SetAchievement("150_FIREBALLS");
            SteamUserStats.StoreStats();
        }

        if(DataPersistance.Fireballs <= 45 && DataPersistance.BossIsDead == 1) //Pásate el juego con solo 45 bolas de fuego o menos
        {
            SteamUserStats.SetAchievement("COMPLETE_GAME_WITH_45_FIREBALLS");
            SteamUserStats.StoreStats();
        }

        if(DataPersistance.MediumAttack == 5) //Se golpeado por un ogro 5 veces
        {
            SteamUserStats.SetAchievement("GET_HIT_5_TIMES__BY_MEDIUM");
            SteamUserStats.StoreStats();
        }

        if (DataPersistance.Bullets == 15) //Se golpeado por el caballero 15 veces
        {
            SteamUserStats.SetAchievement("GET_HIT_20_TIMES__BY_HARD");
            SteamUserStats.StoreStats();
        }

        if(DataPersistance.Time <= 390 && SceneManager.GetActiveScene().name == "Credits") //Completa el juego en 6 minutos
        {
            SteamUserStats.SetAchievement("WIN_IN_TIME");
            SteamUserStats.StoreStats();
        }

        if(DataPersistance.TotalAttack == 1) //Sube tu ataque al máximo
        {
            SteamUserStats.SetAchievement("MAX_ATTACK");
            SteamUserStats.StoreStats();
        }

        if (DataPersistance.TotalDefense == 1) //Sube tu defensa al máximo
        {
            SteamUserStats.SetAchievement("MAX_DEFENSE");
            SteamUserStats.StoreStats();
        }

        if (DataPersistance.TotalBoost == 1) //Sube tu Boost al máximo
        {
            SteamUserStats.SetAchievement("MAX_BOOST");
            SteamUserStats.StoreStats();
        }

        if(DataPersistance.TotalAttack == 1 && DataPersistance.TotalDefense == 1 && DataPersistance.TotalBoost == 1) //Compra todo lo de la tienda y deja sin stock a Robert
        {
            SteamUserStats.SetAchievement("OUT_OF_STOCK");
            SteamUserStats.StoreStats();
        }

        if(DataPersistance.TotalAttack == 0 && DataPersistance.TotalDefense == 0 && DataPersistance.TotalBoost == 0 && SceneManager.GetActiveScene().name == "Level_Boss") //No compres nada en la tienda
        {
            SteamUserStats.SetAchievement("DONT_BUY_ROUTE");
            SteamUserStats.StoreStats();
        }

        if(DataPersistance.ItemsCollected == 20)
        {
            SteamUserStats.SetAchievement("ALL_ITEMS_COLLECTED");
            SteamUserStats.StoreStats();
        }

        if (!Input.GetKeyDown(KeyCode.Space)) { return; }

        SteamUserStats.SetAchievement("ACH_WIN_ONE_GAME");
        SteamUserStats.StoreStats();

    }
}
