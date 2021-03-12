// TO-DO: Clean up (and maybe rewrite the code).

/*--------------------------------------------------------------
// ObjectPooling.cs
//
// Created by Mark Placzek 
// https://www.raywenderlich.com/847-object-pooling-in-unity
// Modified by Milandro Jr. Tolentino
//  
// Copyright © 2021 Team Dynamyte. All rights reserved.
--------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public int amountToPool;
    public GameObject objectToPool;
    //public bool shouldExpand;
}

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling SharedInstance;
    public List<GameObject> pooledObjects;
    public List<GameObject> pooledEnemies;
    public List<ObjectPoolItem> itemsToPool;
    public List<ObjectPoolItem> enemiesToPool;

    private void Awake()
    {
        SharedInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }

        pooledEnemies = new List<GameObject>();
        foreach (ObjectPoolItem enemy in enemiesToPool)
        {
            for (int i = 0; i < enemy.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(enemy.objectToPool);
                obj.SetActive(false);
                pooledEnemies.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(string tag)
    {
        // Credits: https://answers.unity.com/questions/1627379/random-object-pooler.html

        if (tag == "Enemy")
        {
            /*for (int i = 0; i < pooledEnemies.Count; i++)
            {
                if (!pooledEnemies[i].activeInHierarchy && pooledEnemies[i].tag == tag)
                {
                    return pooledEnemies[Random.Range(i, pooledEnemies.Count)];
                }
            }
            return null;
            */

            // Filter the list of pooled object and put all the inactive ones into a new list
            List<GameObject> inActiveObjects = pooledEnemies.FindAll(go => !go.activeInHierarchy);

            // Check if the list created above has elements
            // If so, pick a random one,
            // Return null otherwise

            return inActiveObjects.Count > 0 ?
                inActiveObjects[Random.Range(0, inActiveObjects.Count)] :
                null;
        }

        else
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
                {

                    return pooledObjects[i];

                }
            }
            return null;
        }

    }
}
