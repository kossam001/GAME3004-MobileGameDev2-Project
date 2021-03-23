using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAI : MonoBehaviour
{
    // Maybe this entire script can be inside enemy behaviour script instead?
    public NavMeshAgent enemy;

    private void OnEnable()
    {
        // When Game Object is enabled, set the agent speed to its normal move speed.
        enemy.speed = 2.5f;
    }
    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(GameObject.FindGameObjectWithTag("Waypoint").transform.position);
    }
}
