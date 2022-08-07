using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioMixer Mixer;

    public const string MusicKey = "MusicVolume";
    public const string SFXKey = "SFXVolume";

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadVolume();
    }

    void LoadVolume() //volume saved on VolumeSettings
    {
        float musicVolume = PlayerPrefs.GetFloat(MusicKey, 0.5f);
        float sfxVolume = PlayerPrefs.GetFloat(SFXKey, 0.5f);

        Mixer.SetFloat(VolumeSettings.MixerMusic, Mathf.Log10(musicVolume) * 20);
        Mixer.SetFloat(VolumeSettings.MixerSFX, Mathf.Log10(sfxVolume) * 20);
    }
}