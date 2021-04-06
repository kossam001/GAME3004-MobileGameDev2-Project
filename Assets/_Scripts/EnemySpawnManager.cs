using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnemySpawnManager : MonoBehaviour
{
    public float enemySpawnWaitTime = 1.5f;

    // For Spawn Enemy button
    public void SpawnEnemy()
    {
        GameStats.Instance.StartWave();
        if (!Pause.gameIsPaused)
        {
            StartCoroutine(SpawnWave());        
        }
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < GameStats.Instance.waveCount; i++)
        {
            GameObject enemy = ObjectPooling.SharedInstance.GetPooledObject("Enemy");
            if (enemy != null)
            {
                enemy.transform.position = transform.position;
                enemy.transform.rotation = transform.rotation;
                enemy.SetActive(true);
            }
            yield return new WaitForSeconds(enemySpawnWaitTime);
        }
    }
}
