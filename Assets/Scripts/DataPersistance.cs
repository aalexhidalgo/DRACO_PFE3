using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistance : MonoBehaviour
{
    //Instancia compartida

    public static DataPersistance DracoState;

    //Variables

    public int MoneyCounter = 0;
    private bool Win = false;

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

    private void SaveForFutureGames()
    {
        //Contador de monedas
        PlayerPrefs.SetInt("Money_Counter", MoneyCounter);
    }
}
