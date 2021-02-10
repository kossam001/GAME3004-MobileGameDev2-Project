using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioMixer audioMixer;

    public static AudioManager instance;

    private void Awake()
    {
        if (AudioManager.instance == null)
        {
            DontDestroyOnLoad(gameObject);
            AudioManager.instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Get music volume
        float musicVol = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        // Set music volume
        AdjustMusicVolume(musicVol);
        // Set SFX volume
        AdjustSFXVolume(sfxVol);
    }

    public void AdjustMusicVolume(float volume)
    {
        // Update AudioMixer
        audioMixer.SetFloat("MusicVolume", volume);
        // Update PlayerPrefs "MusicVolume"
        PlayerPrefs.SetFloat("MusicVolume", volume);

        //Debug.Log("PlayerPrefs saved as: " + volume);

        // Save changes
        PlayerPrefs.Save();
    }

    public void AdjustSFXVolume(float volume)
    {
        // Update AudioMixer
        audioMixer.SetFloat("SFXVolume", volume);
        // Update PlayerPrefs "SFXVolume"
        PlayerPrefs.SetFloat("SFXVolume", volume);

        //Debug.Log("PlayerPrefs saved as: " + volume);

        // Save changes
        PlayerPrefs.Save();
    }
}
