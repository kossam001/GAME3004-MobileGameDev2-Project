using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScareCrowTowerBehaviour : Tower
{

    private Transform target;

    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 10.0f;

    private string originalTargetTag;
    public GameObject childObjectContainingTheTargetTag;

    GameObject[] enemies;

    void Awake()
    {
        originalTargetTag = childObjectContainingTheTargetTag.tag;
    }

    private void OnEnable()
    {
        target = null;
    }

    private void OnDisable()
    {
        CancelInvoke("UpdateTarget");
        if (target != null)
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<NavMeshAgent>().speed = enemy.GetComponent<EnemyBehaviour>().defaultSpeed;
            }
        }
    }

    public void TurnOn()
    {
        childObjectContainingTheTargetTag.tag = originalTargetTag;

        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
    }

    void UpdateTarget()
    {
        enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;

                if (nearestEnemy != null && shortestDistance <= range)
                {
                    // Slow enemy
                    target = nearestEnemy.transform;
                    enemy.GetComponent<NavMeshAgent>().speed = Mathf.Clamp(1 / ((float) strength * 0.5f), 0.1f, 1.0f);
                }
                else
                {
                    if (target != null)
                    {
                        enemy.GetComponent<NavMeshAgent>().speed = enemy.GetComponent<EnemyBehaviour>().defaultSpeed;
                    }

                    target = null;
                }
            }
            else if (distanceToEnemy > range)
            {
                if (enemy != null)
                {
                    enemy.GetComponent<NavMeshAgent>().speed = enemy.GetComponent<EnemyBehaviour>().defaultSpeed;
                }
            }
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
