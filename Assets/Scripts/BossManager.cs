using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    private AudioSource MainCameraAudioSource;
    private AudioSource BossManagerAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        MainCameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        MainCameraAudioSource.volume = DataPersistance.DracoState.MusicVolume;
        BossManagerAudioSource = GetComponent<AudioSource>();
        BossManagerAudioSource.volume = DataPersistance.DracoState.SoundVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
