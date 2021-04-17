using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// Original Health Script taken from Jason Weimann Unity tutorial

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;

    [SerializeField] private int enemyCoinDrop = 100;

    public int currentHealth;

    public event Action<float> OnHealthPctChanged = delegate { };

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void ModifyHealth(int amount)
    {
        currentHealth += amount;

        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        OnHealthPctChanged(currentHealthPct);

        if (currentHealth <= 0)
        {
            if (GetComponent<EnemyBehaviour>())
            {
                StatisticsTracker.Instance.UpdateStats(4, 1);
                GameStats.Instance.AddResources(0, 0, 0, enemyCoinDrop);
            }

            // Maybe play a sound here??

            List<Transform> tileTransforms = new List<Transform>();

            foreach (TowerTile go in GameObject.FindObjectsOfType<TowerTile>())
            {
                tileTransforms.Add(go.GetComponent<Transform>());
            }

            GetClosestTile(tileTransforms.ToArray());

            this.gameObject.SetActive(false);
        }
    }

    Transform GetClosestTile(Transform[] enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        bestTarget.gameObject.GetComponent<TowerTile>().objectType = ObjectType.NONE;

        return bestTarget;
    }

    private void OnDisable()
    {
        currentHealth = 0;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        OnHealthPctChanged(1);
    }

    public bool IsMaxHealth()
    {
        return currentHealth == maxHealth;
    }
}