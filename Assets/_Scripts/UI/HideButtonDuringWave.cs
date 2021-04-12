using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideButtonDuringWave : MonoBehaviour
{
    public GameObject[] buttons;
    EnemySpawnManager enemySpawnManager;

    private void Start()
    {
        enemySpawnManager = (EnemySpawnManager)FindObjectOfType(typeof(EnemySpawnManager));
    }

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0 && !enemySpawnManager.allEnemiesSpawned)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(false);
            }
        }

        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0 && enemySpawnManager.allEnemiesSpawned)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(true);
            }
        }
    }
}
