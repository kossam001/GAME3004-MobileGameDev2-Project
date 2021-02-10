using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    //public Slider slider;

    private void Start()
    {
        Slider slider = GetComponent<Slider>();

        slider.value = PlayerPrefs.GetFloat("MusicSliderValue", 0.75f);
    }

    public void SetMusicVolume(float sliderValue)
    {
        // NOTE: Because of the way AudioMixers use a logarithmic scale, we need to store seperate values
        PlayerPrefs.SetFloat("MusicSliderValue", sliderValue);
        PlayerPrefs.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20.0f);

        AudioManager.instance.AdjustMusicVolume(Mathf.Log10(sliderValue) * 20.0f);
    }
}
