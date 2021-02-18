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
    public List<ObjectPoolItem> itemsToPool;

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
    }

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        //foreach (ObjectPoolItem item in itemsToPool)
        //{
        //    if (item.objectToPool.tag == tag)
        //    {
        //        if (item.shouldExpand)
        //        {
        //            GameObject obj = (GameObject)Instantiate(item.objectToPool);
        //            obj.SetActive(false);
        //            pooledObjects.Add(obj);
        //            return obj;
        //        }
        //    }
        //}
        return null;
    }

    //public void ReturnObject(GameObject returnedObject)
    //{
    //    returnedObject.SetActive(false);
    //}
}
