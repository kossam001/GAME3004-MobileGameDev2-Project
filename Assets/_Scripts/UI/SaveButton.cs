using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveButton : MonoBehaviour
{
    GameObject[] gos;

    public void SaveGame()
    {
        //GameObject.FindObjectOfType

        PlayerPrefs.SetInt("NumWood", GameStats.Instance.resource1);
        PlayerPrefs.SetInt("NumFood", GameStats.Instance.resource2);
        PlayerPrefs.SetInt("NumIron", GameStats.Instance.resource3);
        PlayerPrefs.SetInt("NumCoins", GameStats.Instance.resource4);
        PlayerPrefs.SetInt("PlayerHealth", GameStats.Instance.health);
        PlayerPrefs.SetInt("NumWaveCount", GameStats.Instance.waveCount);

        List<TowerTile> unsortedTowerTiles = new List<TowerTile>();

        foreach (TowerTile go in GameObject.FindObjectsOfType<TowerTile>())
        {
            //tileObjectTypes.Add(go.GetComponent<TowerTile>().objectType);
            unsortedTowerTiles.Add(go);

            //Debug.Log(go.GetComponent<TowerTile>().objectType);
        }

        //TowerTile[] unsortedTowerTilesArray = unsortedTowerTiles.ToArray();

        //TowerTile[] sortedTowerTilesArray = unsortedTowerTilesArray.OrderBy(go => go.name).ToArray();

        //ObjectType[] sortedObjectTypes = new ObjectType[sortedTowerTilesArray.Length];

        //for (int i = 0; i < sortedTowerTilesArray.Length; i++)
        //{
        //    //sortedObjectTypes[i] = sortedTowerTilesArray[i].objectType;

        //    //Debug.Log(sortedTowerTilesArray[i].name + " " + sortedObjectTypes[i]);
        //}



        //SaveCurrentTileValues(sortedObjectTypes, "TileObjectTypes");
        SaveCurrentTileValues(unsortedTowerTiles);

        PlayerPrefs.Save();

        Debug.Log("GAME SAVED");
    }

    void SaveCurrentTileValues(List<TowerTile> arrayToSave)
    {
        // key: tilename; value: ObjectType,range,firerate,strength,etc.
        for (int i = 0; i < arrayToSave.Count; i++)
        {
            GameObject tower = arrayToSave[i].towerOnTile;

            if (!tower)
            {
                // Just to overwrite previous tile data
                PlayerPrefs.SetString(arrayToSave[i].name, "");
            }
            else
            {
                Tower towerData = tower.GetComponent<Tower>();

                string saveString = ((int)arrayToSave[i].objectType).ToString() + "," + towerData.range + "," + towerData.fireRate + "," + towerData.strength + ",";

                PlayerPrefs.SetString(arrayToSave[i].name, saveString);
            }

            //PlayerPrefs.SetInt(SaveName + "TotalLength", arrayToSave.Length);
        }
    }
}