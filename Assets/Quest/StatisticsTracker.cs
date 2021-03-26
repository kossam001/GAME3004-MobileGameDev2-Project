using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsTracker : MonoBehaviour
{
    private static StatisticsTracker instance;
    public static StatisticsTracker Instance { get { return instance; } }

    public List<Statistics> stats;
    public Dictionary<int, Statistics> statTable;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void UpdateStats(int ID, int progress)
    {
        stats[ID].UpdateProgress(progress);
    }

    public Statistics GetStatistic(int ID)
    {
        return stats[ID];
    }

    private void OnDisable()
    {
        // Format: id,progress...
        string saveStr = "";

        foreach (Statistics stat in statTable.Values)
        {
            saveStr += stat.id.ToString() + ",";
            saveStr += stat.progress.ToString();
        }

        PlayerPrefs.SetString("GameStatistics", saveStr);
        PlayerPrefs.Save();
    }

    private void OnEnable()
    {
        statTable = new Dictionary<int, Statistics>();

        foreach (Statistics stat in stats)
        {
            statTable[stat.id] = stat;
        }

        string loadedData = PlayerPrefs.GetString("GameStatistics", "");

        if (loadedData == "") return;

        char[] delimiters = new char[] { ',' };
        string[] splitData = loadedData.Split(delimiters);

        for (int i = 0; i < statTable.Values.Count; i++)
        {
            int dataIdx = i * 2;

            int id = int.Parse(splitData[dataIdx]);
            int progress = int.Parse(splitData[dataIdx + 1]);

            statTable[id].progress = progress;
        }
    }
}
