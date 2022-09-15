using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GamePadController : MonoBehaviour
{
    public int Keyboard_Controller = 0;
    public int PS4_Controller = 0;
    public int Xbox_One_Controller = 0;
    public string[] names;

    private void Awake()
    {

        names = Input.GetJoystickNames();
        for (int x = 0; x < names.Length; x++)
        {
            //Debug.Log(names[x].Length);

            if (names[x].Length == 19)
            {
                Debug.Log("PS4 CONTROLLER IS CONNECTED");
                PS4_Controller = 1;
                Keyboard_Controller = 0;
            }

            if (names[0].Length == 0)
            {
                Debug.Log("ANY CONTROLLER IS CONNECTED");
                //set a controller bool to true
                PS4_Controller = 0;
                Keyboard_Controller = 1;
            }

        }
    }

    void Update()
    {
        names = Input.GetJoystickNames();
        for (int x = 0; x < names.Length; x++)
        {
            //Debug.Log(names[x].Length);

            if (names[x].Length == 19)
            {
                Debug.Log("PS4 CONTROLLER IS CONNECTED");
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

            if (names[0].Length == 0)
            {
                Debug.Log("ANY CONTROLLER IS CONNECTED");
                //set a controller bool to true
                PS4_Controller = 0;
                Xbox_One_Controller = 0;
                Keyboard_Controller = 1;
            }
        }

        if(PS4_Controller == 0 || Xbox_One_Controller == 0)
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