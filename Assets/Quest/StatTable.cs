using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatTable", menuName = "Quests/StatTable")]
public class StatTable : ScriptableObject
{
    public List<Statistics> allStats;

    public Statistics getStat(int id)
    {
        return allStats[id];
    }
}
