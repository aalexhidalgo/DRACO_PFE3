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
    public bool DialogueAnimDone = false;
    public bool CanClick = true;

    public GameObject Attack_Image;
    public GameObject Defense_Image;
    public GameObject Boost_Image;

    //Partículas
    public ParticleSystem[] EntranceParticleSystem;
    public ParticleSystem ChooseParticleSystem;
    private Vector3 Pos1 = new Vector3(-435f, 183.899994f, 675.330017f);
    private Vector3 Pos2 = new Vector3(-17.60001f, 183.899994f, 675.330017f);
    private Vector3 Pos3 = new Vector3(423.2f, 183.899994f, 675.330017f);


    //Money
    public TextMeshProUGUI MoneyText;
    private int propValue;

    private float Increment = 0.25f;

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
        DialogueText.text = DialogoList[DataPersistance.DracoState.CurrentLevel-1][CurrentDialogueText];
        MoneyText.text = DataPersistance.DracoState.MoneyCounter.ToString();
        DataPersistance.DracoState.Storedone = 0;
        DataPersistance.DracoState.SaveForFutureGames();
    }

    #region Borrar
    public void Level_1()
    {
        DataPersistance.DracoState.CurrentLevel = 0;
        DataPersistance.DracoState.Storedone = 1;
        DataPersistance.DracoState.SaveForFutureGames();
        Debug.Log("Level 1");
        SceneManager.LoadScene("Level_1");
    }

    public void Level_2()
    {
        DataPersistance.DracoState.CurrentLevel = 1;
        DataPersistance.DracoState.Storedone = 1;
        DataPersistance.DracoState.SaveForFutureGames();
        Debug.Log("Level 2");
        SceneManager.LoadScene("Level_2");
    }

    public void Level_3()
    {
        DataPersistance.DracoState.CurrentLevel = 2;
        DataPersistance.DracoState.Storedone = 1;
        DataPersistance.DracoState.SaveForFutureGames();
        Debug.Log("Level 3");
        SceneManager.LoadScene("Level_3");
    }

    public void Level_4()
    {
        DataPersistance.DracoState.CurrentLevel = 3;
        DataPersistance.DracoState.Storedone = 1;
        DataPersistance.DracoState.SaveForFutureGames();
        Debug.Log("Level 4");
        SceneManager.LoadScene("Level_4");
    }

    public void Level_Boss()
    {
        DataPersistance.DracoState.CurrentLevel = 4;
        DataPersistance.DracoState.Storedone = 1;
        DataPersistance.DracoState.SaveForFutureGames();
        Debug.Log("Level Boss");
        SceneManager.LoadScene("Level_Boss");
    }
    #endregion

    public void ContinueButton()
    {
        DataPersistance.DracoState.Storedone = 1;
        SceneManager.LoadScene(DataPersistance.DracoState.CurrentLevel);
        DataPersistance.DracoState.SaveForFutureGames();
    }

    //Despertamos al vendedor, que nos hablará
    public void ShowDialogue()
    {
        if(CanClick)
        {
            DialogueImage.SetActive(true);
            StartCoroutine(Letters());
        }  
    }

    public void NextButton()
    {
        //Hasta que no se haya acabado de reproducir el diálogo, el jugador no podrá darle a next.
        if(DialogueAnimDone == true)
        {
            CurrentDialogueText++;

            if (CurrentDialogueText >= DA1.Length)
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
                DialogueText.text = DialogoList[DataPersistance.DracoState.CurrentLevel - 1][CurrentDialogueText];
                StartCoroutine(Letters());
            }
        }
        
    }

    //Aparición del diálogo por letras
    private IEnumerator Letters()
    {
        DialogueAnimDone = false;
        CanClick = false;

        string Originalmessage = DialogueText.text;

        DialogueText.text = "";

        foreach (var d in Originalmessage) //var (comodín)
        {
            DialogueText.text += d;
            yield return new WaitForSeconds(0.05f);
        }

        DialogueAnimDone = true;
    }

    public void YesButton_1()
    {
        if(DialogueAnimDone == true)
        {
            if (DataPersistance.DracoState.MoneyCounter >= propValue)
            {
                PayMoney(propValue);
                UpdateMoney();
                DataPersistance.DracoState.Fireball--;
                DataPersistance.DracoState.FireballValue+= Increment;
                DialogueText.text = "Gracias por comprar! ";

                if (DataPersistance.DracoState.Fireball == 0)
                {
                    StartCoroutine(YesButtonCoroutine(Pos1, "Fireball_prefab"));
                }
            }
            else
            {
                DialogueText.text = "No tienes suficiente dinero. No doy nada gratis. Querias algo? ";
            }
        }
    }

    public void YesButton_2()
    {
        if (DialogueAnimDone == true)
        {

            if (DataPersistance.DracoState.MoneyCounter >= propValue)
            {
                PayMoney(propValue);
                UpdateMoney();
                DataPersistance.DracoState.Shield--;
                DataPersistance.DracoState.ShieldValue ++;

                DialogueText.text = "Gracias por comprar! ";

                if (DataPersistance.DracoState.Shield == 0)
                {
                    StartCoroutine(YesButtonCoroutine(Pos2, "Escudo_prefab")); //Hacer 3D
                }

            }
            else
            {
                DialogueText.text = "No tienes suficiente dinero. No doy nada gratis. Querias algo? ";
            }
        }
    }

    public void YesButton_3()
    {
        if (DialogueAnimDone == true)
        {
            if (DataPersistance.DracoState.MoneyCounter >= propValue)
            {
                PayMoney(propValue);
                UpdateMoney();
                DataPersistance.DracoState.Fly--;
                DataPersistance.DracoState.FlyValue += Increment;
                DialogueText.text = "Gracias por comprar! ";

                if (DataPersistance.DracoState.Fly == 0)
                {
                    StartCoroutine(YesButtonCoroutine(Pos3, "Nube_prefab")); //Hacer 3D
                }
            }
            else
            {
                DialogueText.text = "No tienes suficiente dinero. No doy nada gratis. Querias algo? ";
            }
        }
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
        if (DialogueAnimDone == true)
        {
            DialogueImage.SetActive(false);
        }
    }

    public void AttackStat_1()
    {
        if (DialogueAnimDone == true)
        {
            propValue = 75;
            DialogueImage.SetActive(true);
            DialogueText.text = $"MMM, BUENA ELECCION...! INCREMENTA EL ATAQUE BASICO EN UN 25%. TE LO PUEDES LLEVAR POR EL PRECIO DE {propValue}€. DESEAS COMPRAR?";
            StartCoroutine(Letters());
            Next.SetActive(false);
            Yes_2.SetActive(false);
            Yes_3.SetActive(false);
            Yes_1.SetActive(true);
            No.SetActive(true);
        }
    }

    public void DefenseStat_2()
    {
        if (DialogueAnimDone == true)
        {
            propValue = 50;
            DialogueImage.SetActive(true);
            DialogueText.text = $"GRAN DEFENSA! INCREMENTA LA DEFENSA BASICA EN UN 25%. TE LO PUEDES LLEVAR POR EL PRECIO DE {propValue}€. DESEAS COMPRAR?";
            StartCoroutine(Letters());
            Next.SetActive(false);
            Yes_1.SetActive(false);
            Yes_3.SetActive(false);
            Yes_2.SetActive(true);
            No.SetActive(true);
        }
    }
    public void BoostStat_3()
    {
        if (DialogueAnimDone == true)
        {
            propValue = 100;
            DialogueImage.SetActive(true);
            DialogueText.text = $"HASTA EL INFINITO Y MAS ALLA! INCREMENTA LA CAPACIDAD DE VUELO EN UN 25%. TE LO PUEDES LLEVAR POR EL PRECIO DE {propValue}€. DESEAS COMPRAR?";
            StartCoroutine(Letters());
            Next.SetActive(false);
            Yes_1.SetActive(false);
            Yes_2.SetActive(false);
            Yes_3.SetActive(true);
            No.SetActive(true);
        }
    }

    public void PayMoney(int value)
    {
        DataPersistance.DracoState.MoneyCounter -= value;
    }

    public void UpdateMoney()
    {
        MoneyText.text = DataPersistance.DracoState.MoneyCounter.ToString();
    }
}
