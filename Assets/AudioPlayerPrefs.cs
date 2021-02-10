using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioPlayerPrefs : MonoBehaviour
{
    [SerializeField]
    AudioMixer audioMixer;
    [SerializeField]
    AudioSource musicAudioSource;

    // There is probably a much better way to do this but this is fine for now...
    void Start()
    {
        audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume", 0.75f));
        audioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume", 0.75f));

        musicAudioSource.volume = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
    }
}
