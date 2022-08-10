using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TutorialManager : MonoBehaviour
{
    public GameObject[] MensajesTutorial;

    private int index;

    private PlayerController playerController;
    private float jumpForceValue;

    private float mov = 0.000001f;

    void Start()
    {
        index = 0;
        ShowTutorial(index);
        playerController = FindObjectOfType<PlayerController>();
        jumpForceValue = playerController.UpSpeed;
        playerController.UpSpeed = 0;
        playerController.canShoot = false;
    }
    void Update()
    {
        if (index == 0)
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > mov)
            {
                StartCoroutine(ShowNext());
            }
        }
        else if (index == 1)
        {
            if (Input.GetButtonDown("UpMove"))
            {
                playerController.UpSpeed = jumpForceValue;
                StartCoroutine(ShowNext());
            }
        }

        else if(index == 2)
        {
            if(Input.GetButtonDown("Fire"))
            {
                playerController.canShoot = true;
                StartCoroutine(ShowNext());
            }
        }
    }
    private void ShowTutorial(int indx)
    {
        for(int i = 0; i<MensajesTutorial.Length; i++)
        {
            if(i == index)
            {
                MensajesTutorial[i].SetActive(true);
                StartCoroutine(FadeIn(i));
            }
            else
            {
                MensajesTutorial[i].SetActive(false);
            }
        }
    }
    private IEnumerator ShowNext()
    {
        index++;
        yield return StartCoroutine(FadeOut(index - 1));
        ShowTutorial(index);
    }

    private IEnumerator FadeIn(int idx)
    {
        GameObject child = MensajesTutorial[idx].transform.GetChild(0).gameObject;
        TextMeshProUGUI textAlpha = child.GetComponent<TextMeshProUGUI>();
        textAlpha.alpha = 0;

        for (float i = 0; i < 1; i += 0.1f)
        {
            textAlpha.alpha = i;
            yield return new WaitForSeconds(0.2f);
        }
        textAlpha.alpha = 1;
    }

    private IEnumerator FadeOut(int idx)
    {
        GameObject child = MensajesTutorial[idx].transform.GetChild(0).gameObject;
        TextMeshProUGUI textAlpha = child.GetComponent<TextMeshProUGUI>();
        textAlpha.alpha = 1;

        for (float i = 1; i > 0; i -= 0.1f)
        {
            textAlpha.alpha = i;
            yield return new WaitForSeconds(0.1f);
        }
        textAlpha.alpha = 0;
    }
}
