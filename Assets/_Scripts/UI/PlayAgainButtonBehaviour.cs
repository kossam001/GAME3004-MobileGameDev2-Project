/*--------------------------------------------------------------
// PlayAgainButtonBehaviour.cs
//
// Handle the event when pressing the PlayAgain Button in GameOver Scene.
//
// Created by Tran Minh Son on Feb 03 2021
// Date last Modified: Feb 03 2021
// Rev: 1.0
//  
// Copyright � 2021 Team Dynamyte. All rights reserved.
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
        FindObjectOfType<AudioManager>().Play("click");
        Debug.Log("Main Menu!");
        StartCoroutine(LoadLevel("DespawnEnemyViaTowerScene", 0.3f));
    }

    // Waiting for _delay seconds to load new scene
    IEnumerator LoadLevel(string _name, float _delay)
    {
        yield return new WaitForSeconds(_delay);
        SceneManager.LoadScene(_name);
    }
}
