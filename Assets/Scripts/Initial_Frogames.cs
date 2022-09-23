using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initial_Frogames : MonoBehaviour
{
    private float Timer = 10f;
    void Start()
    {
        StartCoroutine(WaitToEnd());
    }

    public IEnumerator WaitToEnd()
    {
        yield return new WaitForSeconds(Timer);
        SceneManager.LoadScene("MainMenu");
    }
}
