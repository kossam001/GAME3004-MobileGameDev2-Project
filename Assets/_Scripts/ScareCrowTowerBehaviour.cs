using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScareCrowTowerBehaviour : MonoBehaviour
{

    private Transform target;
    public float range = 10.0f;

    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        // NOTE: This would need to be changed in some way to make sure this doesn't happen multiple times... (ie. continuously raising/lowering the speed value)
        if (nearestEnemy != null && shortestDistance <= range)
        {
            // Make the enemy "scared"
            target = nearestEnemy.transform;
            target.GetComponent<NavMeshAgent>().speed = 1;
        }
        else
        {
            if (target != null)
            {
                Debug.Log("Target left scarecrow range");
                target.GetComponent<NavMeshAgent>().speed = target.GetComponent<EnemyBehaviour>().defaultSpeed;
            }

            target = null;
        }

        // NOTE: The invoke will continue to be called even if the GameObject is deactivated. This is here to make sure the invoke is cancelled.
        if (GetComponent<Health>().currentHealth <= 0)
        {
            // Make sure the speed is reset if the enemy was still in the radius when the tower was destroyed
            target.GetComponent<NavMeshAgent>().speed = target.GetComponent<EnemyBehaviour>().defaultSpeed;

            CancelInvoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        // Lock on Target Enemy
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0.0f, rotation.y, 0.0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
