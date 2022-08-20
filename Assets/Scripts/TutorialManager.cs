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
    private GamePadController GamePadControllerScript;

    private float jumpForceValue;

    private float mov = 0.000001f;

    public TextMeshProUGUI Tutorialtext1;
    public TextMeshProUGUI Tutorialtext2;
    public TextMeshProUGUI Tutorialtext3;
    public TextMeshProUGUI Tutorialtext4;

    public GameObject[] TutorialButtonImages;


    void Start()
    {
        index = 0;
        ShowTutorial(index);
        playerController = FindObjectOfType<PlayerController>();
        GameManagerScript = FindObjectOfType<GameManager>();
        GamePadControllerScript = FindObjectOfType<GamePadController>();
        jumpForceValue = playerController.UpSpeed;
        playerController.UpSpeed = 0;
        playerController.canShoot = false;
    }
    void Update()
    {
        if (GamePadControllerScript.PS4_Controller == 0)
        {
            Tutorialtext1.text = "Prueba a usar las teclas A y D o las flechas para moverte a los lados";
            Tutorialtext2.text = "Usa la barra espaciadora, la W o la flecha hacia arriba para saltar";
            Tutorialtext3.text = "Pulsa E para lanzar una bola de fuego";
            Tutorialtext4.text = "Agarra la nube y pulsa Q para consumir la barra de vuelo";

            foreach (GameObject i in TutorialButtonImages)
            {
                i.SetActive(false);
            }
        }
        else
        {
            Tutorialtext1.text = "Usa         o            para moverte a los lados";
            Tutorialtext2.text = "Usa         para saltar";
            Tutorialtext3.text = "Presiona            para lanzar una bola de fuego";
            Tutorialtext4.text = "Agarra la nube y presiona            para consumir la barra de vuelo";

            foreach(GameObject i in TutorialButtonImages)
            {
                i.SetActive(true);
            }
        }

        if (index == 0) //si te mueves desaparece el primer tip
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > mov && playerController.DracoCanMov)
            {
                StartCoroutine(ShowNext());
            }
        }
        else if (index == 1) //si saltas desaparece el segundo tip
        {
            if (Input.GetButtonDown("UpMove") && playerController.UpSpeed > 0)
            {
                StartCoroutine(ShowNext());
            }
        }

        else if(index == 2) //si disparas fuego desaparece el tercer tip
        {
            if(Input.GetButtonDown("Fire") && playerController.canShoot)
            {
                StartCoroutine(ShowNext());
            }
        }

        else if(index == 3) //si coges la nube desaparece el �ltimo tip
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
        
        TextMeshProUGUI textAlpha = child.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI KeyTutorial = child.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();

        textAlpha.alpha = Alphavalue;

        while (Alphavalue <= 1)
        {
            boxColor.a = Alphavalue;
            childImage.color = boxColor;

            textAlpha.alpha = Alphavalue;
            KeyTutorial.alpha = Alphavalue;

            foreach (GameObject i in TutorialButtonImages)
            {
                Image ButtonImage = i.GetComponent<Image>();
                Color ImageColor = ButtonImage.color;
                ImageColor.a = Alphavalue;
                ButtonImage.color = ImageColor;
            }


            Alphavalue += 0.075f;
            yield return new WaitForSeconds(0.1f);
        }

        boxColor.a = Alphavalue;
        childImage.color = boxColor;

        textAlpha.alpha = Alphavalue;
        KeyTutorial.alpha = Alphavalue;

        if (index == 0)
        {
            playerController.DracoCanMov = true;
        }

        if (index == 1)
        {
            playerController.UpSpeed = jumpForceValue;
        }

        else if (index == 2) //si disparas fuego desaparece el tercer tip
        {
            playerController.canShoot = true;
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
        float Alphavalue = 1;

        GameObject child = MensajesTutorial[idx].transform.GetChild(0).gameObject;
        Image childImage = child.GetComponent<Image>();
        Color boxColor = childImage.color;

        TextMeshProUGUI textAlpha = child.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI KeyTutorial = child.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();

        textAlpha.alpha = Alphavalue;

        while (Alphavalue > 0)
        {
            boxColor.a = Alphavalue;
            childImage.color = boxColor;

            textAlpha.alpha = Alphavalue;
            KeyTutorial.alpha = Alphavalue;

            foreach (GameObject i in TutorialButtonImages)
            {
                Image ButtonImage = i.GetComponent<Image>();
                Color ImageColor = ButtonImage.color;
                ImageColor.a = Alphavalue;
                ButtonImage.color = ImageColor;
            }

            Alphavalue -= 0.075f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
