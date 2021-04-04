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
    [SerializeField] private GameObject questPanel;

    private List<Quest> allQuests;

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
        allQuests = new List<Quest>();

        foreach (Quest quest in quests.allQuests)
        {
            GameObject questDescription = Instantiate(questDescriptionPrefab, questPanel.transform);
            TMP_Text questText = questDescription.GetComponentInChildren<TMP_Text>();

            quest.questObject = questDescription;
            quest.SetDisplay(quest);

            allQuests.Add(quest);
        }

        if (StatisticsTracker.Instance == null) return;

        //foreach (Quest quest in quests.allQuests)
        //{
            //Statistics stat = StatisticsTracker.Instance.GetStatistic(quest.associatedStatID);
            //stat.OnProgressUpdated += quest.UpdateProgress;
            //quest.Initialize(stat.GetProgress());
            //quest.Initialize(0);
        //}
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        foreach (Quest quest in allQuests)
            StatisticsTracker.Instance.GetStatistic(quest.associatedStatID).OnProgressUpdated -= quest.UpdateProgress;
    }

    public void UpdateQuest(int questID, int progressAmount)
    {
        Quest quest = quests.getQuest(questID);
        quest.UpdateProgress(progressAmount);

        quest.SetDisplay(quest);
    }

    public List<Quest> getQuests()
    {
        return allQuests;
    }
}
