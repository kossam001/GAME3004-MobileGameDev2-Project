using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;

    public TMP_Text waveText;
    private int waveCounter;

    // Update is called once per frame
    void Update()
    {     
        // Spawns Enemy (Game View Only since spacebar)
        if (Input.GetKeyDown(KeyCode.Space) && !Pause.gameIsPaused)
        {
            Debug.Log("SpawnManager Heard Space");

            // Instantiate enemy prefab between the two points
            GameObject enemy = ObjectPooling.SharedInstance.GetPooledObject("Enemy");
            if (enemy != null)
            {
                enemy.transform.position = transform.position;
                enemy.transform.rotation = transform.rotation;
                enemy.SetActive(true);

                enemy.GetComponent<NavMeshAgent>().speed = enemy.GetComponent<EnemyBehaviour>().defaultSpeed;
            }
        }
    }

    // For Spawn Enemy button
    public void SpawnEnemy()
    {
        if (!Pause.gameIsPaused)
        {
            waveCounter++;
            waveText.text = "Wave: " + waveCounter;
            GameObject enemy = ObjectPooling.SharedInstance.GetPooledObject("Enemy");
            if (enemy != null)
            {
                enemy.transform.position = transform.position;
                enemy.transform.rotation = transform.rotation;
                enemy.SetActive(true);
            }
        }
    }
}
