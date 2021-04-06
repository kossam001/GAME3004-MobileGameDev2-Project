using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
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
            GameStats.Instance.resource4 = PlayerPrefs.GetInt("NumCoin");
            GameStats.Instance.health = PlayerPrefs.GetInt("PlayerHealth");
            GameStats.Instance.waveCount = PlayerPrefs.GetInt("NumWaveCount");

            GameStats.Instance.UpdateResourcesUI();

            // NOTE: Need to make sure the tiles are reset somehow

            GameObject[] goArray = GetObjectsInLayer(6);

            if (goArray != null)
            {
                for (int i = 0; i < goArray.Length; i++)
                {
                    goArray[i].gameObject.SetActive(false);
                }
            }

            // Create list of ObjectTypes
            List<ObjectType> tileObjectTypes = new List<ObjectType>();

            // For each TowerTile in the scene
            foreach (TowerTile go in GameObject.FindObjectsOfType<TowerTile>())
            {
                // Add the scene's TowerTile's ObjectTypes to our list of ObjectTypes
                tileObjectTypes.Add(go.GetComponent<TowerTile>().objectType);
            }

            // Create Tower Tile Array with correct size (number of TowerTiles in scene)
            TowerTile[] towerTileArray = new TowerTile[tileObjectTypes.Count];
            // Create List of TowerTiles
            List<TowerTile> towerTiles = new List<TowerTile>();

            // Create ObjectType array with correct size (number of TowerTiles in scene)
            ObjectType[] tileObjectTypesArray = new ObjectType[tileObjectTypes.Count];

            // Create array of loaded TowerTile Object Types
            tileObjectTypesArray = GetSavedObjectTypeFromString("TileObjectTypes");

            // For each TowerTile in the scene
            foreach (TowerTile go in GameObject.FindObjectsOfType<TowerTile>())
            {
                // Add the scene's TowerTiles to the TowerTile list
                towerTiles.Add(go);
            }

            // Put the TowerTile list into the TowerTile array
            towerTileArray = towerTiles.ToArray();

            TowerTile[] towerTileTestArray = towerTileArray.OrderBy(go => go.name).ToArray();

            for (int i = 0; i < tileObjectTypesArray.Length; i++)
            {
                towerTileTestArray[i].objectType = tileObjectTypesArray[i];

                // Update the TowerTile to display/instantiate the appropriate object on it
                towerTileTestArray[i].UpdateTile();

                //Debug.Log(towerTileArray[i]);
            }

            Debug.Log("GAME LOADED");
        }
    }

    private void Start()
    {
        LoadButtonBehaviour.loadGameOnStartup = false;
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