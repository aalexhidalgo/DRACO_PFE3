using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BetweenLevelsManager : MonoBehaviour
{
    public GameObject LevelPanel;
    public GameObject StorePanel;

    void Start()
    {
        
    }

    public void Level_1()
    {
        Debug.Log("Level 1");
        SceneManager.LoadScene("Level_1");
    }

    public void Level_2()
    {
        Debug.Log("Level 2");
        SceneManager.LoadScene("Level_2");
    }

    public void Level_3()
    {
        Debug.Log("Level 3");
        SceneManager.LoadScene("Level_3");
    }

    public void Level_4()
    {
        Debug.Log("Level 4");
        SceneManager.LoadScene("Level_4");
    }

    public void Level_Boss()
    {
        Debug.Log("Level Boss");
        SceneManager.LoadScene("Level_Boss");
    }

    public void ContinueButton()
    {
        LevelPanel.SetActive(true);
        StorePanel.SetActive(false);
    }
}
