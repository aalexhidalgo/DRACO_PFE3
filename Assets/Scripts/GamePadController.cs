using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Internal;

public class GamePadController : MonoBehaviour
{
    public int Keyboard_Controller = 0;
    public int PS4_Controller = 0;
    public int Xbox_One_Controller = 0;
    public string[] names;
    

    private void Awake()
    {
        names = Input.GetJoystickNames();

        #region ControllerFor
        /*
        for (int x = 0; x < names.Length; x++)
        {
            //Debug.Log(names[x].Length);

            if (names[x].Length == 19)
            {
                Debug.Log("PS4 CONTROLLER IS CONNECTED IN AWAKE");
                PS4_Controller = 1;
                Xbox_One_Controller = 0;
                Keyboard_Controller = 0;
            }

            if (names[x].Length == 33)
            {
                Debug.Log("XBOX CONTROLLER IS CONNECTED");
                //set a controller bool to true
                PS4_Controller = 0;
                Xbox_One_Controller = 1;
                Keyboard_Controller = 0;
            }

            if (names[0].Length == 0 && names[1].Length == 0)
            {
                Debug.Log("ANY CONTROLLER IS CONNECTED");
                //set a controller bool to true
                PS4_Controller = 0;
                Xbox_One_Controller = 0;
                Keyboard_Controller = 1;
            }

        }
        */
        #endregion

        //caso 1 nunca has enchufado un mando, caso 2 has enchufado 1, caso 3 has enchufado 2 mandos
        if (names.Length == 0 || names[0].Length == 0 && names.Length == 1 || (names[0].Length == 0 && names[1].Length == 0 && names.Length == 2))
        {
            //Debug.Log("ANY CONTROLLER IS CONNECTED");

            PS4_Controller = 0;
            Xbox_One_Controller = 0;
            Keyboard_Controller = 1;
        }

        //caso 1 solo has jugado con mando de PlayStation, caso 2 has jugado con 2 mandos Xbox y Play
        foreach (string name in names)
        {
            //Si has enchufado un mando de Play
            if ((name == "Wireless Controller")) //name.Lenght == 19;
            {
                //Debug.Log("IN UPDATE PS4 CONTROLLER IS CONNECTED IN AWAKE");
                PS4_Controller = 1;
                Xbox_One_Controller = 0;
                Keyboard_Controller = 0;
            }
            //Si has enchufado un mando de Xbox
            else if (name == "Controller (Xbox One For Windows)") //name.Lenght == 33;
            {
                //Debug.Log("XBOX CONTROLLER IS CONNECTED IN AWAKE");

                PS4_Controller = 0;
                Xbox_One_Controller = 1;
                Keyboard_Controller = 0;
            }
        }

    }

    void Update()
    {
        names = Input.GetJoystickNames();

        //caso 1 nunca has enchufado un mando, caso 2 has enchufado 1, caso 3 has enchufado 2 mandos
        if (names.Length == 0 || names[0].Length == 0 && names.Length == 1 || (names[0].Length == 0 && names[1].Length == 0 && names.Length == 2))
        {
            //Debug.Log("ANY CONTROLLER IS CONNECTED");
            
            PS4_Controller = 0;
            Xbox_One_Controller = 0;
            Keyboard_Controller = 1;
        }


        
        foreach (string name in names)
        {
            //Si tienes un mando enchufado de Play
            if (name == "Wireless Controller")
            {
                //Debug.Log("IN UPDATE PS4 CONTROLLER IS CONNECTED IN UPDATE");
                PS4_Controller = 1;
                Xbox_One_Controller = 0;
                Keyboard_Controller = 0;
            }
            //Si tienes un mando enchufado de Xbox
            else if (name == "Controller (Xbox One For Windows)")
            {
                //Debug.Log("XBOX CONTROLLER IS CONNECTED IN UPDATE");
                
                PS4_Controller = 0;
                Xbox_One_Controller = 1;
                Keyboard_Controller = 0;
            }
        }


        if (PS4_Controller == 0 && Xbox_One_Controller == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //EventSystem.current.SetSelectedGameObject(null);
        }

        if (PS4_Controller == 1 || Xbox_One_Controller == 1)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

}