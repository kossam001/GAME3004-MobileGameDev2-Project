using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public string tileName;
    public string towerName;
    public TowerTile tile;

    [Header("Attributes")]
    public float range = 10.0f;
    public float fireRate = 1.0f;
    public float strength = 1.0f;

    public float maxRange = 20.0f;
    public float maxFireRate = 5.0f;
    public float maxStrength = 5.0f;

    public int[] upgradeCost = { 100, 100, 100, 100 };
    public int[] repairCost = { 100, 100, 100, 100 };
    public int[] resourceCost = { 100, 100, 100, 100 };

    public int SellPrice;

    public void SetResourceCost(int[] costs)
    {
        for (int i = 0; i < costs.Length; i++)
        {
            resourceCost[i] = costs[i];
        }
    }

    public bool IsMaxStats()
    {
        if (IsMaxRange() &&
            IsMaxFireRate() &&
            IsMaxStrength())
        {
            return true;
        }

        return false;
    }

    public bool IsMaxRange()
    {
        if (range >= maxRange) return true;

        return false;
    }

    public bool IsMaxFireRate()
    {
        if (fireRate >= maxFireRate) return true;

        return false;
    }

    public bool IsMaxStrength()
    {
        if (strength >= maxStrength) return true;

        return false;
    }
}
