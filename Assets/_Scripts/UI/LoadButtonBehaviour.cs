/*--------------------------------------------------------------
// LoadButtonBehaviour.cs
//
// Handle the event when pressing the Load Button in MainMenu Scene.
//
// Created by Tran Minh Son on Feb 03 2021
// Date last Modified: Feb 03 2021
// Rev: 1.0
//  
// Copyright © 2021 Team Dynamyte. All rights reserved.
--------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadButtonBehaviour : MonoBehaviour
{
    public static bool loadGameOnStartup = false;
    public GameObject Alert;

    public void OnLoadButtonPressed()
    {
        if (PlayerPrefs.HasKey("NumWood")
                && PlayerPrefs.HasKey("NumFood")
                && PlayerPrefs.HasKey("NumIron")
                && PlayerPrefs.HasKey("NumCoins")
                && PlayerPrefs.HasKey("PlayerHealth")
                && PlayerPrefs.HasKey("NumWaveCount"))
        {
            Debug.Log("Load Button Pressed!");
            loadGameOnStartup = true;

            SceneManager.LoadScene("DespawnEnemyViaTowerScene");
        }
        else
        {
            Alert.SetActive(true);
            StartCoroutine(ShowAlert());
        }


    }

    IEnumerator ShowAlert()
    {
        yield return new WaitForSeconds(1.0f);
        Alert.SetActive(false);
    }
}
