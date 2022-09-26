using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FireLogic_Boss : MonoBehaviour
{
    public float Speed = 10f;

    //Particles
    public ParticleSystem DamageParticleSystem;

    //Scripts
    private BossLogic BossLogicScript;
    private PlayerController PlayerControllerScript;
    private GameManager GameManagerScript;


    //public Volume PostProcesadoDaño;
    private Vignette vg;
    /*
    public AudioClip HitDraco;
    public AudioSource AudioManagerAudioSource;
    */

    void Start()
    {
        PlayerControllerScript = FindObjectOfType<PlayerController>();
        GameManagerScript = FindObjectOfType<GameManager>();
        BossLogicScript = FindObjectOfType<BossLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagerScript.pause == false)
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
        }
    }

    public void OnTriggerEnter(Collider otherTrigger)
    {

        if (otherTrigger.gameObject.CompareTag("Player") && BossLogicScript.Win == false)
        {
            if (PlayerControllerScript.Shield == 1)
            {
                StartCoroutine(PlayerControllerScript.DracoDamaged());
                PlayerControllerScript.MaxShieldValue -= 1;
                if (PlayerControllerScript.MaxShieldValue <= 0)
                {
                    PlayerControllerScript.Shield = 0;
                    PlayerControllerScript.UpdateShield();
                    Debug.Log("Te quedas sin escudo crack");
                }
                else
                {
                    PlayerControllerScript.UpdateShieldImage();
                }
            }

            else
            {
                PlayerControllerScript.CurrentLive -= 0.5f;
                StartCoroutine(PlayerControllerScript.DracoDamaged());
                if (PlayerControllerScript.CurrentLive <= 0)
                {
                    PlayerControllerScript.CurrentLive = 0;

                    GameManagerScript.restartButton.Select();
                    GameManagerScript.GameOver = true;
                    GameManagerScript.GameOverPanel.SetActive(true);
                    PlayerControllerScript.UpdateLife();
                }
                
                PlayerControllerScript.UpdateLife();
                Destroy(transform.parent.gameObject);
            }
        }

        if(otherTrigger.gameObject.CompareTag("Fire"))
        {
            Instantiate(DamageParticleSystem, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(transform.parent.gameObject);
            Destroy(otherTrigger.gameObject);
        }

        if(otherTrigger.gameObject.CompareTag("Ground") || otherTrigger.gameObject.CompareTag("Plataformas"))
        {
            Instantiate(DamageParticleSystem, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(transform.parent.gameObject);
        }

        if (otherTrigger.gameObject.CompareTag("Wall"))
        {
            Instantiate(DamageParticleSystem, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(transform.parent.gameObject);
        }

    }


    /*public IEnumerator DracoDamaged()
    {
        PostProcesadoDaño.profile.TryGet(out vg);
        vg.intensity.value = 0f;
        while (vg.intensity.value < 0.8f)
        {
            vg.intensity.value += 0.1f;
            yield return new WaitForSeconds(0.05f);
        }
        while (vg.intensity.value > 0)
        {
            vg.intensity.value -= 0.1f;
            yield return new WaitForSeconds(0.05f);
        }
    }
    */
    /*private void OnDestroy()
    {
        Instantiate(DamageParticleSystem, gameObject.transform.position, gameObject.transform.rotation);
    }
    */
}
