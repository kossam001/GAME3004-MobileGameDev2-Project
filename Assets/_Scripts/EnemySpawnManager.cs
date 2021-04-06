using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnemySpawnManager : MonoBehaviour
{
    public TMP_Text waveText;
    private int waveCounter = 0;

    // Update is called once per frame
    void Update()
    {     

    }

    // For Spawn Enemy button
    public void SpawnEnemy()
    {
        if (!Pause.gameIsPaused)
        {
            StartCoroutine(SpawnWave());
            waveText.text = "Wave: " + waveCounter;
         
        }
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < waveCounter; i++)
        {
            GameObject enemy = ObjectPooling.SharedInstance.GetPooledObject("Enemy");
            if (enemy != null)
            {
                enemy.transform.position = transform.position;
                enemy.transform.rotation = transform.rotation;
                enemy.SetActive(true);
            }
            yield return new WaitForSeconds(1.5f);
        }
        waveCounter++; 
    }
}
