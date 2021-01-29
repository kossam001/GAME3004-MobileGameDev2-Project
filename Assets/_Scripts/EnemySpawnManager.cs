using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    Transform northSpawnPost;
    [SerializeField]
    Transform southSpawnPost;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("SpawnManager Heard Space");

            // Instantiate enemy prefab between the two points
        }
    }
}
