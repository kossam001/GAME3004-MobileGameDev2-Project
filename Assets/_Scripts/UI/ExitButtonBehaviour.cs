/*--------------------------------------------------------------
// ExitButtonBehaviour.cs
//
// Handle the event when pressing the Exit Button in MainMenu Scene.
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

public class ExitButtonBehaviour : MonoBehaviour
{
    public void OnExitButtonPressed()
    {
        Debug.Log("Exit!");
        StartCoroutine(DelayExit(0.3f));
    }

    // Waiting for _delay seconds to load new scene
    IEnumerator DelayExit(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        Application.Quit();
    }
}
