using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideButtonDuringWave : MonoBehaviour
{
   
    private EnemyBehaviour[] enemy;
    public GameObject[] buttons;

    void Update()
    {
        enemy = FindObjectsOfType<EnemyBehaviour>();
        if (enemy.Length > 0)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(false);
            }
        }

        else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(true);
            }
        }
    }
}
