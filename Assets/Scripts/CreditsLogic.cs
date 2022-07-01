using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsLogic : MonoBehaviour
{
    //Maybe incluir alguna cinemática o animación

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Salimos del juego
    public void ExitButton()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
