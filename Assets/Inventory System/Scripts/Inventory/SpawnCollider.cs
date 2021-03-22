using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollider : MonoBehaviour
{
    public GameObject spawnInObject;
    public bool isObstructed = false;
    public bool isOnTile = false;
    public LayerMask layerMask;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger &&
            other.gameObject.layer == LayerMask.NameToLayer("Object") &&
            !ReferenceEquals(other.gameObject, spawnInObject))
        {
            isObstructed = true;

            Debug.Log(other.gameObject.name);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger &&
            other.gameObject.layer == LayerMask.NameToLayer("Object") &&
            !ReferenceEquals(other.gameObject, spawnInObject))
        {
            isObstructed = false;

            Debug.Log(other.gameObject.name);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000, layerMask))
            {
                Debug.Log(hit.transform.gameObject.name);
            }
        }
    }
}
