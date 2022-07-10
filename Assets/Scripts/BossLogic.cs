using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class BossLogic : MonoBehaviour
{
    [SerializeField] private float speed = 30;

    //Puntos a seguir
    [SerializeField] private Transform[] points;
    private int totalPoints;
    private int nextPoint;

    public GameObject Fireball;
    public Transform PlayerTransform;
    public bool canMove;

    public float BossLife = 20f; //vida del boss
    public bool Win = false;
    public Image LifeBoss; 
    public float MaxBossLife = 20f;

    public int ShieldBoss;
    public GameObject ShieldBossImage;
    public Image ShieldStateBoss;
    public Sprite[] ShieldBossSprites;
    public int ShieldValueBoss;

    private AudioSource GameManagerAudioSource;
    public AudioClip Boss_Death_Clip;

    public ParticleSystem Fireworks_1;
    public ParticleSystem Fireworks_2;
    public ParticleSystem Fireworks_3;
    public ParticleSystem Fireworks_4;

    //Comunicación con scripts
    private GameManager GameManagerScript;

    void Awake()
    {

    }
    void Start()
    {
        transform.position = points[0].position;
        totalPoints = points.Length;
        nextPoint = 1;
        canMove = true;

        LifeBoss.GetComponent<Image>();
        UpdateShield();
        UpdateShieldImage();

        GameManagerScript = FindObjectOfType<GameManager>();
        GameManagerAudioSource = GameObject.Find("GameManager").GetComponent<AudioSource>();

    //transform.LookAt(points[nextPoint].position);

    //Colisión con pared que nos permita hacer los puntos de ruta (if blablabla)
    //StartCoroutine(BossAttack());

    }

    void Update()
    {
        if(canMove && Win == false)
        {
            if (GameManagerScript.pause == false)
            {
                if (Vector3.Distance(transform.position, points[nextPoint].position) < 0.1f)
                {
                    canMove = false;

                    nextPoint++;

                    if (nextPoint == totalPoints)
                    {
                        nextPoint = 0;
                    }
                    StartCoroutine(BossAttack());
                    //transform.LookAt(points[nextPoint].position);
                }

                transform.position = Vector3.MoveTowards(transform.position, points[nextPoint].position, speed * Time.deltaTime);
            }   
        }

        transform.GetChild(0).transform.LookAt(PlayerTransform);
    }

    public IEnumerator BossAttack()
    {
        float Timer = 0.5f;
        int SpawnRate = Random.Range(0, 4);
        yield return new WaitForSeconds(Timer);

        for (int i = 0; i <= SpawnRate; i++)
        {
            Instantiate(Fireball, transform.GetChild(0).transform.position, transform.GetChild(0).transform.rotation);
            yield return new WaitForSeconds(Timer);
        }
        canMove = true;
    }

    public void OnTriggerEnter (Collider otherTrigger)
    {
        if(DetectBoss.SharedInstance.Damage == true)
        {
            if (otherTrigger.gameObject.CompareTag("Fire") && ShieldBoss == 0)
            {
                BossLife -= DataPersistance.DracoState.FireballValue;
                LifeBoss.fillAmount = BossLife / MaxBossLife;
                Destroy(otherTrigger.gameObject);

                if (BossLife <= 0)
                {
                    BossLife = 0;
                    LifeBoss.fillAmount = BossLife / MaxBossLife;
                    StartCoroutine(Boss_Death());
                }
            }

            if (otherTrigger.gameObject.CompareTag("Fire") && ShieldBoss == 1)
            {
                ShieldValueBoss--;
                Destroy(otherTrigger.gameObject);

                if (ShieldValueBoss <= 0)
                {
                    ShieldBoss = 0;
                    UpdateShield();
                }
                else
                {
                    UpdateShieldImage();
                }

            }

            if (otherTrigger.gameObject.CompareTag("Shield_Boss"))
            {
                ShieldBoss = 1;
                ShieldValueBoss++;
                Destroy(otherTrigger.gameObject);
                UpdateShield();
                UpdateShieldImage();
            }
        }
    }

    public void UpdateShield()
    {
        if (ShieldBoss == 1)
        {
            ShieldBossImage.SetActive(true);
        }
        else
        {
            ShieldBossImage.SetActive(false);
        }
    }

    public void UpdateShieldImage()
    {
        if (ShieldValueBoss <= 1)
        {
            ShieldBossImage.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            ShieldBossImage.transform.GetChild(0).gameObject.SetActive(true);
            ShieldStateBoss.sprite = ShieldBossSprites[ShieldValueBoss - 2];
        }
    }

    public IEnumerator Boss_Death()
    {
        //Cambio sprite
        GameManagerAudioSource.PlayOneShot(Boss_Death_Clip);
        Win = true;
        Fireworks_1.Play();
        yield return new WaitForSeconds(0.6f);
        Fireworks_2.Play();
        yield return new WaitForSeconds(0.6f);
        Fireworks_3.Play();
        yield return new WaitForSeconds(0.6f);
        Fireworks_4.Play();
        yield return new WaitForSeconds(0.6f);
       
        float Timer = 2.1f;
        yield return new WaitForSeconds(Timer);
        SceneManager.LoadScene("Credits");
    }
}
