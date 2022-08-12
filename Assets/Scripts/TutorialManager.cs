using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class TutorialManager : MonoBehaviour
{
    public GameObject[] MensajesTutorial;

    private int index;

    private PlayerController playerController;
    private GameManager GameManagerScript;

    private float jumpForceValue;

    private float mov = 0.000001f;

    void Start()
    {
        index = 0;
        ShowTutorial(index);
        playerController = FindObjectOfType<PlayerController>();
        GameManagerScript = FindObjectOfType<GameManager>();
        jumpForceValue = playerController.UpSpeed;
        playerController.UpSpeed = 0;
        playerController.canShoot = false;
    }
    void Update()
    {
        if (index == 0) //si te mueves desaparece el primer tip
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > mov)
            {
                StartCoroutine(ShowNext());
            }
        }
        else if (index == 1) //si saltas desaparece el segundo tip
        {
            if (Input.GetButtonDown("UpMove"))
            {
                playerController.UpSpeed = jumpForceValue;
                StartCoroutine(ShowNext());
            }
        }

        else if(index == 2) //si disparas fuego desaparece el tercer tip
        {
            if(Input.GetButtonDown("Fire"))
            {
                playerController.canShoot = true;
                StartCoroutine(ShowNext());
            }
        }

        else if(index == 3) //si coges la nube desaparece el último tip
        {
            if(GameManagerScript.FlybarCounter>0)
            {
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
        float Alphavalue = 0;

        GameObject child = MensajesTutorial[idx].transform.GetChild(0).gameObject;
        Image childImage = child.GetComponent<Image>();
        Color boxColor = childImage.color;

        GameObject DracoImage = child.transform.GetChild(1).gameObject;
        SpriteRenderer ImageDraco = DracoImage.GetComponent<SpriteRenderer>();
        Color DracoColor = ImageDraco.color;
        
        TextMeshProUGUI textAlpha = child.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

        textAlpha.alpha = Alphavalue;

        while (Alphavalue <= 1)
        {
            boxColor.a = Alphavalue;
            childImage.color = boxColor;

            DracoColor.a = Alphavalue;
            ImageDraco.color = DracoColor;

            textAlpha.alpha = Alphavalue;
            Alphavalue += 0.1f;
            yield return new WaitForSeconds(0.2f);
        }

        /*for (float i = 0; i < 1; i += 0.1f)
        {
            boxColor.a= i;
            textAlpha.alpha = i;
            //DracoColor.a = i;
            yield return new WaitForSeconds(0.2f);
        }
        */
    }

    private IEnumerator FadeOut(int idx)
    {
        GameObject child = MensajesTutorial[idx].transform.GetChild(0).gameObject;
        TextMeshProUGUI textAlpha = child.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        //GameObject DracoImage = MensajesTutorial[idx].transform.GetChild(1).gameObject;

        Color boxColor = child.GetComponent<Image>().color;
        //Color DracoColor = DracoImage.GetComponent<Image>().color;

        textAlpha.alpha = 1;
        boxColor.a = 1;
        //DracoColor.a = 0;

        for (float i = 1; i > 0; i -= 0.1f)
        {
            boxColor.a = i;
            textAlpha.alpha = i;
            //DracoColor.a = i;
            yield return new WaitForSeconds(0.1f);
        }
        boxColor.a = 0;
    }
}
