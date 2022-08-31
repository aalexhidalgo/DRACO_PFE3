using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    //AudioMixer que controla el volumen
    public AudioMixer Mixer;

    //Sliders que revisaremos si cambian de valor
    public Slider MusicSlider;
    public Slider SFXSlider;

    //nombre de los exposed parameter del volumen
    public const string MixerMusic = "MusicVolume"; 
    public const string MixerSFX = "SFXVolume";

    private void Awake()
    {
        //AddListener asocia una funcion al slider, en ese caso una que se ejecuta cuando su valor cambia
        //MusicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        //SFXSlider = GameObject.Find("SoundSlider").GetComponent<Slider>();

        MusicSlider.onValueChanged.AddListener(SetMusicVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);

    }

    private void Start()
    {
        MusicSlider.value = PlayerPrefs.GetFloat(AudioManager.MusicKey, 0.5f);
        SFXSlider.value = PlayerPrefs.GetFloat(AudioManager.SFXKey, 0.5f);
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.MusicKey, MusicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.SFXKey, SFXSlider.value);
    }
    void SetMusicVolume(float value)
    {
        Mixer.SetFloat(MixerMusic, Mathf.Log10(value) * 20);
        DataPersistance.MusicVolume = MusicSlider.value;
    }

    void SetSFXVolume(float value)
    {
        Mixer.SetFloat(MixerSFX, Mathf.Log10(value) * 20);
        DataPersistance.SoundVolume = SFXSlider.value;
    }

}
