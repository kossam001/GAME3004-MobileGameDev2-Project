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

    private bool hasObject = false;

    public void UpdateTile()
    {
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
                GameObject test = Instantiate(prefabs[0], transform);
                test.GetComponent<AttackTowerBehaviour>().TurnOn();
                hasObject = true;
                break;
            case ObjectType.WINDMILLTOWER:
                GameObject test2 = Instantiate(prefabs[1], transform);
                test2.GetComponent<AttackTowerBehaviour>().TurnOn();
                hasObject = true;
                break;
            case ObjectType.SCARECROWTOWER:
                Instantiate(prefabs[2], transform);
                GameObject test3 = Instantiate(prefabs[2], transform);
                test3.GetComponent<ScareCrowTowerBehaviour>().TurnOn();
                hasObject = true;
                break;
            case ObjectType.PLANT:
                Instantiate(prefabs[3], transform);
                hasObject = true;
                break;
        }
    }
}