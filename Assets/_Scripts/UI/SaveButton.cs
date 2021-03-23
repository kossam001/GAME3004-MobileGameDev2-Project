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

        List<TowerTile> unsortedTowerTiles = new List<TowerTile>();

        foreach (TowerTile go in GameObject.FindObjectsOfType<TowerTile>())
        {
            //tileObjectTypes.Add(go.GetComponent<TowerTile>().objectType);
            unsortedTowerTiles.Add(go);

            //Debug.Log(go.GetComponent<TowerTile>().objectType);
        }

        TowerTile[] unsortedTowerTilesArray = unsortedTowerTiles.ToArray();

        TowerTile[] sortedTowerTilesArray = unsortedTowerTilesArray.OrderBy(go => go.name).ToArray();

        ObjectType[] sortedObjectTypes = new ObjectType[sortedTowerTilesArray.Length];

        for (int i = 0; i < sortedTowerTilesArray.Length; i++)
        {
            sortedObjectTypes[i] = sortedTowerTilesArray[i].objectType;

            //Debug.Log(sortedTowerTilesArray[i].name + " " + sortedObjectTypes[i]);
        }

        SaveCurrentTileValues(sortedObjectTypes, "TileObjectTypes");

        PlayerPrefs.Save();

        Debug.Log("GAME SAVED");
    }

    void SaveCurrentTileValues(ObjectType[] arrayToSave, string SaveName)
    {
        for (int i = 0; i < arrayToSave.Length; i++)
        {

            PlayerPrefs.SetInt(SaveName + i, (int)arrayToSave[i]);

            PlayerPrefs.SetInt(SaveName + "TotalLength", arrayToSave.Length);
        }
    }
}