using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LoadButtonGameplay : MonoBehaviour
{
    public void LoadGame()
    {
        GameStats.Instance.resource1 = PlayerPrefs.GetInt("NumWood");
        GameStats.Instance.resource2 = PlayerPrefs.GetInt("NumFood");
        GameStats.Instance.resource3 = PlayerPrefs.GetInt("NumIron");
        GameStats.Instance.resource4 = PlayerPrefs.GetInt("NumCoins");
        GameStats.Instance.health = PlayerPrefs.GetInt("PlayerHealth");
        GameStats.Instance.waveCount = PlayerPrefs.GetInt("NumWaveCount");

        GameStats.Instance.UpdateResourcesUI();

        // Need to make sure the tiles are reset somehow

        GameObject[] goArray = GetObjectsInLayer(6);

        if (goArray != null)
        {
            for (int i = 0; i < goArray.Length; i++)
            {
                goArray[i].gameObject.SetActive(false);
            }
        }

        // Create list of ObjectTypes
        //List<ObjectType> tileObjectTypes = new List<ObjectType>();

        // For each TowerTile in the scene
        //foreach (TowerTile go in GameObject.FindObjectsOfType<TowerTile>())
        //{
        //    // Add the scene's TowerTile's ObjectTypes to our list of ObjectTypes
        //    tileObjectTypes.Add(go.GetComponent<TowerTile>().objectType);
        //}

        TowerTile[] towerTileList = GameObject.FindObjectsOfType<TowerTile>();
        LoadTowers(towerTileList);


        //// Create Tower Tile Array with correct size (number of TowerTiles in scene)
        //TowerTile[] towerTileArray = new TowerTile[tileObjectTypes.Count];
        //// Create List of TowerTiles
        //List<TowerTile> towerTiles = new List<TowerTile>();

        //// Create ObjectType array with correct size (number of TowerTiles in scene)
        //ObjectType[] tileObjectTypesArray = new ObjectType[tileObjectTypes.Count];

        //// Create array of loaded TowerTile Object Types
        //tileObjectTypesArray = GetSavedObjectTypeFromString("TileObjectTypes");

        // For each TowerTile in the scene
        //foreach (TowerTile go in GameObject.FindObjectsOfType<TowerTile>())
        //{
        //    // Add the scene's TowerTiles to the TowerTile list
        //    towerTiles.Add(go);
        //}

        // Put the TowerTile list into the TowerTile array
        //towerTileArray = towerTiles.ToArray();

        //TowerTile[] towerTileTestArray = towerTileArray.OrderBy(go => go.name).ToArray();

        //for (int i = 0; i < tileObjectTypesArray.Length; i++)
        //{
        //    towerTileTestArray[i].objectType = tileObjectTypesArray[i];

        //    // Update the TowerTile to display/instantiate the appropriate object on it
        //    towerTileTestArray[i].UpdateTile();

        //    //Debug.Log(towerTileArray[i]);
        //}

        Debug.Log("GAME LOADED");
    }

    //public ObjectType[] GetSavedObjectTypeFromString(string baseSaveName)
    //{
    //    ObjectType[] ReconstructedArrayFromName = new ObjectType[PlayerPrefs.GetInt(baseSaveName + "TotalLength")];

    //    for (int i = 0; i < ReconstructedArrayFromName.Length; i++)
    //    {
    //        ReconstructedArrayFromName[i] = (ObjectType)PlayerPrefs.GetInt(baseSaveName + i);
    //    }

    //    return ReconstructedArrayFromName;
    //}

    public void LoadTowers(TowerTile[] tiles)
    {
        // key: tilename; value: ObjectType,range,firerate,strength,etc.

        foreach (TowerTile tile in tiles)
        {
            string loadedData = PlayerPrefs.GetString(tile.name, "");

            if (loadedData == "")
            {
                tile.objectType = ObjectType.NONE;
                tile.UpdateTile();
            }
            else
            {
                string[] parsedData = loadedData.Split(',');

                tile.objectType = (ObjectType)Int32.Parse(parsedData[0]);
                tile.UpdateTile();

                Tower towerData = tile.towerOnTile.GetComponent<Tower>();
                towerData.range = float.Parse(parsedData[1]);
                towerData.fireRate = float.Parse(parsedData[2]);
                towerData.strength = float.Parse(parsedData[3]);
                towerData.tile = tile;
            }
        }
    }

    GameObject[] GetObjectsInLayer(int layer)
    {
        var goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        var goList = new System.Collections.Generic.List<GameObject>();

        for (int i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer == layer)
            {
                goList.Add(goArray[i]);
            }
        }
        if (goList.Count == 0)
        {
            return null;
        }

        return goList.ToArray();
    }
}