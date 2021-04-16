using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsTracker : MonoBehaviour
{
    private static StatisticsTracker instance;
    public static StatisticsTracker Instance { get { return instance; } }

    public StatTable stats;
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

        statTable = new Dictionary<int, Statistics>();

        // Wait for QuestTracker to initialize first
        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        while (QuestTracker.Instance == null && QuestTracker.Instance.getQuests().Count <= 0)
        {
            yield return null;
        }

        foreach (Statistics stat in stats.allStats)
        {
            statTable[stat.id] = stat;
            stat.SetProgress(0);

            foreach (Quest quest in QuestTracker.Instance.getQuests())
            {
                if (quest.associatedStatID == stat.id)
                {
                    stat.OnProgressUpdated += quest.UpdateProgress;
                    quest.Initialize(0);
                }
            }
        } 

        if (LoadButtonBehaviour.loadGameOnStartup == true) Load();
    }

    public void UpdateStats(int ID, int progress)
    {
        stats.getStat(ID).UpdateProgress(progress);
    }

    public Statistics GetStatistic(int ID)
    {
        return stats.getStat(ID);
    }

    public void Save()
    {
        // Format: id,progress...
        string saveStr = "";

        foreach (Statistics stat in statTable.Values)
        {
            saveStr += stat.id.ToString() + ",";
            saveStr += stat.GetProgress().ToString() + ",";
        }

        PlayerPrefs.SetString("GameStatistics", saveStr);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        string loadedData = PlayerPrefs.GetString("GameStatistics", "");

        if (loadedData == "") return;

        char[] delimiters = new char[] { ',' };
        string[] splitData = loadedData.Split(delimiters);

        for (int i = 0; i < statTable.Values.Count; i++)
        {
            int dataIdx = i * 2;

            int id = int.Parse(splitData[dataIdx]);
            int progress = int.Parse(splitData[dataIdx + 1]);

            statTable[id].SetProgress(progress);
        }
    }
}
