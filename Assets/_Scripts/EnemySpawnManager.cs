using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    Transform northSpawnPost;
    [SerializeField]
    //Transform southSpawnPost;

    // Update is called once per frame
    void Update()
    {     
        // Spawns Enemy (Game View Only since spacebar)
        if (Input.GetKeyDown(KeyCode.Space))
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
        GameObject enemy = ObjectPooling.SharedInstance.GetPooledObject("Enemy");
        if (enemy != null)
        {
            enemy.transform.position = transform.position;
            enemy.transform.rotation = transform.rotation;
            enemy.SetActive(true);
        }
    }
}
