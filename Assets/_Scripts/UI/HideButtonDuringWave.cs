using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideButtonDuringWave : MonoBehaviour
{
    // I'll probably change this so it greys out instead of disabling the gameobject and hiding it... 
    // TO-DO: Make it work for any buttons and not hardcode the button.

    private EnemyBehaviour[] enemy;
    public GameObject startWaveButton;
    //public GameObject[] buttons;

    void Update()
    {
        enemy = FindObjectsOfType<EnemyBehaviour>();
        //Debug.Log(enemy.Length);
        if (enemy.Length > 0)
        {
            //Debug.Log(enemy.Length);
            startWaveButton.SetActive(false);
        }

        else
        {
            //Debug.Log(enemy.Length);
            startWaveButton.SetActive(true);
            //if(gameObject.activeInHierarchy == false)
            //{
            //    gameObject.SetActive(true);
            //}
        }
    }
}
