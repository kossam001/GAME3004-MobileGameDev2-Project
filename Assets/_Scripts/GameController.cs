using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GameController : MonoBehaviour
{
    [Header("Bullets Related")]
    public int MaxFireballs;
    public BulletType bucketType;
    public BulletType pumpkinType;

    private static GameController instance;
    public static GameController Instance { get { return instance; } }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        if (LoadButtonBehaviour.loadGameOnStartup == true)
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

            TowerTile[] towerTileList = GameObject.FindObjectsOfType<TowerTile>();
            LoadTowers(towerTileList);

            Debug.Log("GAME LOADED");
        }
    }

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
                towerData.strength = int.Parse(parsedData[3]);
                towerData.tile = tile;
            }
        }
    }

    private void Start()
    {
        LoadButtonBehaviour.loadGameOnStartup = false;

        // Prepare Bullet pool using by Towers
        BulletPooling.Instance().Init(MaxFireballs, bucketType, pumpkinType);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !Pause.gameIsPaused)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                InteractableObject clickedObject = hit.collider.gameObject.GetComponent<InteractableObject>();
                if (clickedObject != null)
                {
                    clickedObject.Use();
                }
            }
        }
    }

    public ObjectType[] GetSavedObjectTypeFromString(string baseSaveName)
    {
        ObjectType[] ReconstructedArrayFromName = new ObjectType[PlayerPrefs.GetInt(baseSaveName + "TotalLength")];

        for (int i = 0; i < ReconstructedArrayFromName.Length; i++)
        {
            ReconstructedArrayFromName[i] = (ObjectType)PlayerPrefs.GetInt(baseSaveName + i);
        }

        return ReconstructedArrayFromName;
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
