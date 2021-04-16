using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    NONE,
    WATCHTOWER,
    WINDMILLTOWER,
    SCARECROWTOWER,
    PLANT
}

public class TowerTile : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabs;

    public ObjectType objectType = ObjectType.NONE;
    public GameObject towerOnTile;

    private bool hasObject = false;

    public void UpdateTile()
    {
        if (hasObject)
        {
            Destroy(towerOnTile);
        }

        switch (objectType)
        {
            case ObjectType.NONE:
                hasObject = false;

                // It would be nice to incorporate the object pooling here somehow

                //if (GetComponentInChildren<AttackTowerBehaviour>() != null)
                //{
                //    Debug.Log("DISABLE THIS TOWER: " + gameObject.GetComponentInChildren<AttackTowerBehaviour>().gameObject.name);
                //    //gameObject.GetComponentInChildren<AttackTowerBehaviour>().gameObject.SetActive(false);
                //    Destroy(gameObject.GetComponentInChildren<AttackTowerBehaviour>().gameObject);
                //}
                //else if (GetComponentInChildren<ScareCrowTowerBehaviour>() != null)
                //{
                //    Debug.Log("DISABLE THIS SCARECROW");
                //    //gameObject.GetComponentInChildren<ScareCrowTowerBehaviour>().gameObject.SetActive(false);
                //    Destroy(gameObject.GetComponentInChildren<ScareCrowTowerBehaviour>().gameObject);
                //}
                //else if (GetComponentInChildren<Resource>() != null)
                //{
                //    Debug.Log("DISABLE THIS PLANT");
                //    //gameObject.GetComponentInChildren<Resource>().gameObject.SetActive(false);
                //    Destroy(gameObject.GetComponentInChildren<Resource>().gameObject);
                //}
                break;
            case ObjectType.WATCHTOWER:
                towerOnTile = Instantiate(prefabs[0], transform);
                towerOnTile.GetComponent<AttackTowerBehaviour>().tileName = name;
                towerOnTile.GetComponent<AttackTowerBehaviour>().TurnOn();
                hasObject = true;
                break;
            case ObjectType.WINDMILLTOWER:
                towerOnTile = Instantiate(prefabs[1], transform);
                towerOnTile.GetComponent<AttackTowerBehaviour>().tileName = name;
                towerOnTile.GetComponent<AttackTowerBehaviour>().TurnOn();
                hasObject = true;
                break;
            case ObjectType.SCARECROWTOWER:
                towerOnTile = Instantiate(prefabs[2], transform);
                towerOnTile.GetComponent<ScareCrowTowerBehaviour>().tileName = name;
                towerOnTile.GetComponent<ScareCrowTowerBehaviour>().TurnOn();
                hasObject = true;
                break;
            case ObjectType.PLANT:
                towerOnTile = Instantiate(prefabs[3], transform);
                towerOnTile.GetComponent<Tower>().tileName = name;
                hasObject = true;
                break;
        }
    }

    public void RemoveTower()
    {
        towerOnTile.SetActive(false);
        towerOnTile = null;
        hasObject = false;
        objectType = ObjectType.NONE;
    }
}