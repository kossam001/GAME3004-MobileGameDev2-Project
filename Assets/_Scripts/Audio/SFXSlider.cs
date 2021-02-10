using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SFXSlider : MonoBehaviour
{
    //Slider slider;

    void Start()
    {
        Slider slider = GetComponent<Slider>();

        slider.value = PlayerPrefs.GetFloat("SFXSliderValue", 0.75f);
    }

    public void SetSFXVolume(float sliderValue)
    {
        // NOTE: Because of the way AudioMixers use a logarithmic scale, we need to store seperate values
        PlayerPrefs.SetFloat("SFXSliderValue", sliderValue);
        PlayerPrefs.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20.0f);

        // Set AudioManager Instance
        AudioManager.instance.AdjustSFXVolume(Mathf.Log10(sliderValue) * 20.0f);
    }
}
