using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestTracker : MonoBehaviour
{
    private static QuestTracker instance;
    public static QuestTracker Instance { get { return instance; } }

    [SerializeField] private QuestLog quests;
    [SerializeField] private GameObject questDescriptionPrefab;
    [SerializeField] private List<Quest> inprogress;
    [SerializeField] private List<Quest> completed;
    [SerializeField] private GameObject questPanel;

    // Start is called before the first frame update
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

        SetupQuestTracker();
    }

    public void SetupQuestTracker()
    {
        foreach (Quest quest in quests.allQuests)
        {
            GameObject questDescription = Instantiate(questDescriptionPrefab);
            TMP_Text questText = questDescription.GetComponentInChildren<TMP_Text>();

            quest.questObject = questDescription;
            quest.SetDisplay(quest);

            if (quest.completed)
            {
                completed.Add(quest);
            }
            else
            {
                inprogress.Add(quest);
            }
        }

        // Place inprogress quests higher up on the list of quests
        foreach (Quest quest in inprogress)
            quest.questObject.transform.SetParent(questPanel.transform);

        // Place completed quests lower in the list
        foreach (Quest quest in completed)
            quest.questObject.transform.SetParent(questPanel.transform);
    }

    private void Start()
    {
        foreach (Quest quest in quests.allQuests)
        {
            Statistics stat = StatisticsTracker.Instance.GetStatistic(quest.associatedStatID);
            stat.OnProgressUpdated += quest.UpdateProgress;
            quest.UpdateProgress(stat.progress);
        }
    }

    private void OnApplicationQuit()
    {
        foreach (Quest quest in completed)
            StatisticsTracker.Instance.GetStatistic(quest.associatedStatID).OnProgressUpdated -= quest.UpdateProgress;
    }

    public void UpdateQuest(int questID, int progressAmount)
    {
        Quest quest = quests.getQuest(questID);
        quest.UpdateProgress(progressAmount);

        quest.SetDisplay(quest);
    }
}
