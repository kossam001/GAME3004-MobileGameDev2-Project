/*--------------------------------------------------------------
// PlayAgainButtonBehaviour.cs
//
// Handle the event when pressing the PlayAgain Button in GameOver Scene.
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

public class PlayAgainButtonBehaviour : MonoBehaviour
{
    public void OnPlayAgainButtonPressed()
    {
        Debug.Log("Play Again");
        StartCoroutine(LoadLevel("DespawnEnemyViaTowerScene", 0.3f));
    }

    // Waiting for _delay seconds to load new scene
    IEnumerator LoadLevel(string _name, float _delay)
    {
        yield return new WaitForSecondsRealtime(_delay);
        SceneManager.LoadScene(_name);
    }
}
