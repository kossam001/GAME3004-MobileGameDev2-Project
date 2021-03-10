using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollider : MonoBehaviour
{
    public GameObject spawnInObject;
    public bool isObstructed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger &&
            other.gameObject.layer == LayerMask.NameToLayer("Object") &&
            !ReferenceEquals(other.gameObject, spawnInObject))
        {
            isObstructed = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger &&
            other.gameObject.layer == LayerMask.NameToLayer("Object") &&
            !ReferenceEquals(other.gameObject, spawnInObject))
        {
            isObstructed = false;
        }
    }
}
