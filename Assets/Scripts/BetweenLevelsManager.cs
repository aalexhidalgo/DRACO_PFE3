using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BetweenLevelsManager : MonoBehaviour
{
    public GameObject LevelPanel;
    public GameObject StorePanel;
    public List<string[]> DialogoList = new List<string[]>();

    //data_persistance???????????????
    public int CurrentLevel = 1;

    //Diálogo
    public GameObject DialogueImage;
    public TextMeshProUGUI DialogueText;
    public GameObject Next;
    public GameObject Yes_1;
    public GameObject Yes_2;
    public GameObject Yes_3;
    public GameObject No;
    public string[] DA1;
    public string[] DA2;
    public string[] DA3;
    public string[] DA4;
    public int CurrentDialogueText;

    public GameObject Attack_Image;
    public GameObject Defense_Image;
    public GameObject Boost_Image;

    //Partículas
    public ParticleSystem[] EntranceParticleSystem;
    public ParticleSystem ChooseParticleSystem;
    private Vector3 Pos1 = new Vector3(-435f, 183.899994f, 675.330017f);
    private Vector3 Pos2 = new Vector3(-17.60001f, 183.899994f, 675.330017f);
    private Vector3 Pos3 = new Vector3(423.2f, 183.899994f, 675.330017f);

    private void Awake()
    {
        DialogoList.Add(DA1);
        DialogoList.Add(DA2);
        DialogoList.Add(DA3);
        DialogoList.Add(DA4);
    }

    void Start()
    {
        Attack_Image.SetActive(false);
        Defense_Image.SetActive(false);
        Boost_Image.SetActive(false);
        DialogueText.text = DialogoList[CurrentLevel-1][CurrentDialogueText];
    }

    public void Level_1()
    {
        CurrentLevel = 1;
        Debug.Log("Level 1");
        SceneManager.LoadScene("Level_1");
    }

    public void Level_2()
    {
        CurrentLevel = 2;
        Debug.Log("Level 2");
        SceneManager.LoadScene("Level_2");
    }

    public void Level_3()
    {
        CurrentLevel = 3;
        Debug.Log("Level 3");
        SceneManager.LoadScene("Level_3");
    }

    public void Level_4()
    {
        CurrentLevel = 4;
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

    public void NextButton()
    {
        CurrentDialogueText++;
        
        //Esta parte no funciona
        if(CurrentDialogueText >= DA1.Length)
        {
            DialogueImage.SetActive(false);
            Attack_Image.SetActive(true);
            Defense_Image.SetActive(true);
            Boost_Image.SetActive(true);

            foreach (ParticleSystem Particulas in EntranceParticleSystem)
            {
                Particulas.Play();
            }
        }
        else
        {
            DialogueText.text = DialogoList[CurrentLevel-1][CurrentDialogueText];
        }
    }

    public void YesButton_1()
    {
        StartCoroutine(YesButtonCoroutine(Pos1, "Fireball_prefab"));
    }

    public void YesButton_2()
    {
        StartCoroutine(YesButtonCoroutine(Pos2, "Escudo_prefab")); //Hacer 3D
    }

    public void YesButton_3()
    {
        StartCoroutine(YesButtonCoroutine(Pos3, "Nube_prefab")); //Hacer 3D
    }

    //Si compramos algo blablabla
    public IEnumerator YesButtonCoroutine(Vector3 Position, string Prefab)
    {
        float Timer = 1.5f;
        //Restar monedas(PlayerController), recoger item, instanciar partículas y minimizar alpha imagen a través de animación???
        Instantiate(ChooseParticleSystem, Position, ChooseParticleSystem.transform.rotation);
        yield return new WaitForSeconds(Timer);
        //Destruir el objeto como si lo hubieras seleccionado
        Destroy(GameObject.Find(Prefab));
    }

    public void NoButton()
    {
        DialogueImage.SetActive(false);
    }

    public void AttackStat_1()
    {
        DialogueImage.SetActive(true);
        DialogueText.text = "MMM, BUENA ELECCION...! INCREMENTA EL ATAQUE BASICO EN UN 25%. TE LO PUEDES LLEVAR POR EL PRECIO DE 75€. DESEAS COMPRAR?";
        Next.SetActive(false);
        Yes_2.SetActive(false);
        Yes_3.SetActive(false);
        Yes_1.SetActive(true);
        No.SetActive(true);
    }

    public void DefenseStat_2()
    {
        DialogueImage.SetActive(true);
        DialogueText.text = "GRAN DEFENSA! INCREMENTA LA DEFENSA BASICA EN UN 25%. TE LO PUEDES LLEVAR POR EL PRECIO DE 50€. DESEAS COMPRAR?";
        Next.SetActive(false);
        Yes_1.SetActive(false);
        Yes_3.SetActive(false);
        Yes_2.SetActive(true);
        No.SetActive(true);
    }
    public void BoostStat_3()
    {
        DialogueImage.SetActive(true);
        DialogueText.text = "HASTA EL INFINITO Y MAS ALLA! INCREMENTA LA CAPACIDAD DE VUELO EN UN 25%. TE LO PUEDES LLEVAR POR EL PRECIO DE 100€. DESEAS COMPRAR?";
        Next.SetActive(false);
        Yes_1.SetActive(false);
        Yes_2.SetActive(false);
        Yes_3.SetActive(true);
        No.SetActive(true);
    }
}
