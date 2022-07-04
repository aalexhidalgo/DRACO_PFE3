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

    public bool isTalking = false;
    public bool isShopping = false;

    public GameObject Attack_Image;
    public GameObject Defense_Image;
    public GameObject Boost_Image;

    //Partículas
    public ParticleSystem[] EntranceParticleSystem;
    public ParticleSystem ChooseParticleSystem;
    private Vector3 Pos1 = new Vector3(-661.599976f, 330f, 711.400024f);
    private Vector3 Pos2 = new Vector3(-250f, 330f, 711.400024f);
    private Vector3 Pos3 = new Vector3(150f, 330f, 711.400024f);


    //Money
    public TextMeshProUGUI MoneyText;
    private int propValue;
    public AudioClip Money;

    private float Increment = 0.25f;

    //Musica y sonido (voz del vendedor cuando te habla)
    private AudioSource MainCameraAudioSource;
    private AudioSource BetweenLevelsManagerAudioSource;

    public Animator VendedorAnim;
    public Image VendedorImage;
    public Sprite VendedorDespierto;

    //Imgágenes de Fuera de Stock
    public GameObject FueradeStock_Fireball;
    public GameObject FueradeStock_Shield;
    public GameObject FueradeStock_Fly;

    //Stock
    private bool closeDialogue;

    private void Awake()
    {
        DialogoList.Add(DA1);
        DialogoList.Add(DA2);
        DialogoList.Add(DA3);
        DialogoList.Add(DA4);
    }

    void Start()
    {
        closeDialogue = false;
        isShopping = false;
        Attack_Image.SetActive(false);
        Defense_Image.SetActive(false);
        Boost_Image.SetActive(false);
        DialogueText.text = DialogoList[DataPersistance.DracoState.CurrentLevel-2][CurrentDialogueText];
        MoneyText.text = DataPersistance.DracoState.MoneyCounter.ToString();
        DataPersistance.DracoState.Storedone = 0;
        DataPersistance.DracoState.SaveForFutureGames();

        MainCameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        MainCameraAudioSource.volume = DataPersistance.DracoState.MusicVolume;


        BetweenLevelsManagerAudioSource = GetComponent<AudioSource>();
        BetweenLevelsManagerAudioSource.volume = DataPersistance.DracoState.SoundVolume;
        BetweenLevelsManagerAudioSource.Stop();

        if(DataPersistance.DracoState.Fireball == 0)
        {
            FueradeStock_Fireball.SetActive(true);
            Attack_Image.SetActive(false);
        }
        if(DataPersistance.DracoState.Shield == 0)
        {
            FueradeStock_Shield.SetActive(true);
            Defense_Image.SetActive(false);
        }
        if(DataPersistance.DracoState.Fly == 0)
        {
            FueradeStock_Fly.SetActive(true);
            Boost_Image.SetActive(false);
        }

        VendedorImage.GetComponent<Image>();

    }

    void Update()
    {
        if(DialogueAnimDone == false && isTalking)
        {
            VendedorImage.sprite = VendedorDespierto; //NO FUNCIONA
        }
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
        //VendedorImage.sprite = VendedorDespierto;

        if (CanClick)
        {
            isTalking = true;
            DialogueImage.SetActive(true);
            StartCoroutine(Letters(Next));
            
            VendedorAnim.SetBool("Talk", true);

        }  
    }

    public void NextButton()
    {
        //Hasta que no se haya acabado de reproducir el diálogo, el jugador no podrá darle a next.
        if (DialogueAnimDone == true)
        {
            Next.SetActive(false);

            if (closeDialogue == false)
            {
                CurrentDialogueText++;
                if (CurrentDialogueText >= DA1.Length)
                {
                    isTalking = false;
                    DialogueImage.SetActive(false);
                    BetweenLevelsManagerAudioSource.Stop();

                    if (DataPersistance.DracoState.Fireball <= 0)
                    {
                        StartCoroutine(FadeIn(FueradeStock_Fireball));
                    }
                    if (DataPersistance.DracoState.Shield <= 0)
                    {
                        StartCoroutine(FadeIn(FueradeStock_Shield));
                    }
                    if (DataPersistance.DracoState.Fly <= 0)
                    {
                        StartCoroutine(FadeIn(FueradeStock_Fly));
                    }

                    if (DataPersistance.DracoState.Fireball > 0)
                    {
                        EntranceParticleSystem[0].Play();
                        Attack_Image.SetActive(true);
                    }
                    if (DataPersistance.DracoState.Shield > 0)
                    {
                        EntranceParticleSystem[1].Play();
                        Defense_Image.SetActive(true);
                    }
                    if (DataPersistance.DracoState.Fly > 0)
                    {
                        EntranceParticleSystem[2].Play();
                        Boost_Image.SetActive(true);
                    }

                }
                else
                {
                    DialogueText.text = DialogoList[DataPersistance.DracoState.CurrentLevel - 2][CurrentDialogueText];
                    StartCoroutine(Letters(Next));
                    BetweenLevelsManagerAudioSource.Play();

                    VendedorAnim.SetBool("Talk", true);
                }
            }
            else
            {
                NoButton();
            }

        }
    }

    //Aparición del diálogo por letras
    private IEnumerator Letters(GameObject Button)
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
        if(isShopping == false)
        {
            Button.SetActive(true);
        }
        else
        {
            Button.SetActive(true);
            No.SetActive(true);
        }
        VendedorAnim.SetBool("Talk", false);
    }

    //Aparición del Cartel de Fuera de Stock
    private IEnumerator FadeIn(GameObject Cartel)
    {
        
        float AlphaValue = 0;       
        Image CartelImage = Cartel.GetComponent<Image>();
        Color Color = CartelImage.color;

        yield return new WaitForSeconds(0.5f);

        while (AlphaValue <= 1)
        {
            Color.a = AlphaValue;
            CartelImage.color = Color;
            AlphaValue += 0.1f;
            yield return new WaitForSeconds(0.075f);
        }

    }

    //Según el prefab que seleccionemos a mejorar, se nos activarán uno botones, que en la condición de sí mostrará:
    public void YesButton_1()
    {
        if(DialogueAnimDone == true)
        {
            if (DataPersistance.DracoState.MoneyCounter >= propValue && DataPersistance.DracoState.Fireball > 0)
            {
                PayMoney(propValue);
                UpdateMoney();
                DataPersistance.DracoState.Fireball--;
                DataPersistance.DracoState.FireballValue+= Increment;
                DialogueText.text = "Gracias por comprar! Quieres comprar otro mas de estos? ";

                if (DataPersistance.DracoState.Fireball == 0)
                {
                    StartCoroutine(YesButtonCoroutine(Pos1, "Fireball_prefab", FueradeStock_Fireball));
                }
            }
            else if (DataPersistance.DracoState.MoneyCounter >= propValue && DataPersistance.DracoState.Fireball <= 0)
            {
                DialogueText.text = "Viva méxico ay ay ay ay";
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

            if (DataPersistance.DracoState.MoneyCounter >= propValue && DataPersistance.DracoState.Shield > 0)
            {
                PayMoney(propValue);
                UpdateMoney();
                DataPersistance.DracoState.Shield--;
                DataPersistance.DracoState.ShieldValue ++;

                DialogueText.text = "Gracias por comprar! Quieres comprar otro mas de estos? ";

                if (DataPersistance.DracoState.Shield == 0)
                {
                    StartCoroutine(YesButtonCoroutine(Pos2, "Escudo_prefab", FueradeStock_Shield));
                }

            }
            else if(DataPersistance.DracoState.MoneyCounter >= propValue && DataPersistance.DracoState.Shield <= 0)
            {
                DialogueText.text = "No quedan gusilus ninio";
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
            if (DataPersistance.DracoState.MoneyCounter >= propValue && DataPersistance.DracoState.Fly > 0)
            {
                PayMoney(propValue);
                UpdateMoney();
                DataPersistance.DracoState.Fly--;
                DataPersistance.DracoState.FlyValue += Increment;
                DialogueText.text = "Gracias por comprar! Quieres comprar otro mas de estos? ";

                if (DataPersistance.DracoState.Fly == 0)
                {
                    StartCoroutine(YesButtonCoroutine(Pos3, "Cloud_prefab", FueradeStock_Fly));
                }
            }
            else if (DataPersistance.DracoState.MoneyCounter >= propValue && DataPersistance.DracoState.Fly <= 0)
            {
                DialogueText.text = "No quedan papas ninio";
            }
            else
            {
                DialogueText.text = "No tienes suficiente dinero. No doy nada gratis. Querias algo? ";
            }
        }
    }

    //Al tener un stock limitado, al comprar el último item este es destruido mostrando su no disponibilidad
    public IEnumerator YesButtonCoroutine(Vector3 Position, string Prefab, GameObject Image)
    {
        float Timer = 1.5f;
        //Restar monedas(PlayerController), recoger item, instanciar partículas y minimizar alpha imagen a través de animación???
        Instantiate(ChooseParticleSystem, Position, ChooseParticleSystem.transform.rotation);
        yield return new WaitForSeconds(Timer);
        //Destruir el objeto como si lo hubieras seleccionado
        Destroy(GameObject.Find(Prefab));
        //Mostramos la imagen de Fuera de Stock    
        Image.SetActive(true);
        StartCoroutine(FadeIn(Image));
        Yes_1.SetActive(false);
        Yes_2.SetActive(false);
        Yes_3.SetActive(false);
        No.SetActive(false);
        closeDialogue = true;
        isShopping = false;
        DialogueText.text = "Me he quedado sin producto, lo siento, elige otra cosa";
        StartCoroutine(Letters(Next));
    }

    public void NoButton()
    {
        if (DialogueAnimDone == true)
        {
            DialogueImage.SetActive(false);
            Yes_1.SetActive(false);
            Yes_2.SetActive(false);
            Yes_3.SetActive(false);
            No.SetActive(false);
            isShopping = true;
        }
    }

    //Prefabs posibles a mejorar

    public void AttackStat_1()
    {
        isShopping = true;
        if (DialogueAnimDone == true)
        {
            if (DataPersistance.DracoState.MoneyCounter >= propValue && DataPersistance.DracoState.Fireball <= 0)
            {
                DialogueText.text = "Nos hemos quedado sin stock, vuelve en otro momento";
            }

            else
            {
                propValue = 75;
                VendedorAnim.SetBool("Talk", true);
                DialogueImage.SetActive(true);
                DialogueText.text = $"MMM, BUENA ELECCION...! INCREMENTA EL ATAQUE BASICO EN UN 25%. TE LO PUEDES LLEVAR POR EL PRECIO DE {propValue}€. DESEAS COMPRAR?";
                StartCoroutine(Letters(Yes_1));
                Next.SetActive(false);
                Yes_2.SetActive(false);
                Yes_3.SetActive(false);
                //Yes_1.SetActive(true);
                //No.SetActive(true);
            }
        }
    }

    public void DefenseStat_2()
    {
        isShopping = true;
        if (DialogueAnimDone == true)
        {
            if (DataPersistance.DracoState.MoneyCounter >= propValue && DataPersistance.DracoState.Shield <= 0)
            {
                DialogueText.text = "Nos hemos quedado sin stock, vuelve en otro momento";
            }

            else
            {
                propValue = 50;
                VendedorAnim.SetBool("Talk", true);
                DialogueImage.SetActive(true);
                DialogueText.text = $"GRAN DEFENSA! INCREMENTA LA DEFENSA BASICA EN UN 25%. TE LO PUEDES LLEVAR POR EL PRECIO DE {propValue}€. DESEAS COMPRAR?";
                StartCoroutine(Letters(Yes_2));
                Next.SetActive(false);
                Yes_1.SetActive(false);
                Yes_3.SetActive(false);
                //Yes_2.SetActive(true);
                //No.SetActive(true);
            }  
        }
    }
    public void BoostStat_3()
    {
        isShopping = true;
        if (DialogueAnimDone == true)
        {
            if (DataPersistance.DracoState.MoneyCounter >= propValue && DataPersistance.DracoState.Fly <= 0)
            {
                DialogueText.text = "Nos hemos quedado sin stock, vuelve en otro momento";
            }

            else
            {
                propValue = 100;
                VendedorAnim.SetBool("Talk", true);
                DialogueImage.SetActive(true);
                DialogueText.text = $"HASTA EL INFINITO Y MAS ALLA! INCREMENTA LA CAPACIDAD DE VUELO. TE LO PUEDES LLEVAR POR EL PRECIO DE {propValue}€. DESEAS COMPRAR?";
                StartCoroutine(Letters(Yes_3));
                Next.SetActive(false);
                Yes_1.SetActive(false);
                Yes_2.SetActive(false);
                //Yes_3.SetActive(true);
                //No.SetActive(true);
            }   

        }
    }

    public void PayMoney(int value)
    {
        DataPersistance.DracoState.MoneyCounter -= value;
    }

    public void UpdateMoney()
    {
        MoneyText.text = DataPersistance.DracoState.MoneyCounter.ToString();
        BetweenLevelsManagerAudioSource.PlayOneShot(Money, 1f);
    }
}
