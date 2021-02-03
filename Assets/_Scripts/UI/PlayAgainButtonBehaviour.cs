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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayAgainButtonPressed()
    {
        Debug.Log("Main Menu!");
        SceneManager.LoadScene("DespawnEnemyViaTowerScene");
    }
}
