using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "Quest", menuName = "Quests/Quest")]
public class Quest : ScriptableObject
{
    public int id;
    public int associatedStatID;
    public string questName;
    public string objective;
    public int progress;
    public int completionAmount;
    public bool completed;

    public GameObject questObject;

    public void Initialize(int loadedProgress)
    {
        progress = loadedProgress;
        progress = Mathf.Clamp(progress, 0, completionAmount);

        if (progress == completionAmount)
        {
            questObject.GetComponentInChildren<TMP_Text>().color = new Color(0.5f, 0.5f, 0.5f, 0.5f); ;
        }
        else
        {
            questObject.GetComponentInChildren<TMP_Text>().color = new Color(1, 1, 1, 1);
        }
    }

    public void UpdateProgress(int progressMade)
    {
        progress = progressMade;
        progress = Mathf.Clamp(progress, 0, completionAmount);

        if (progress == completionAmount)
        {
            questObject.GetComponentInChildren<TMP_Text>().color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        }
        else
        {
            questObject.GetComponentInChildren<TMP_Text>().color = new Color(1, 1, 1, 1);
        }

        SetDisplay(this);
    }

    public void SetDisplay(Quest quest)
    {
        TMP_Text questText = questObject.GetComponentInChildren<TMP_Text>();

        questText.text = quest.questName + "\n";
        questText.text += quest.objective + " ";
        questText.text += "\nProgress: " + quest.progress + " / " + quest.completionAmount + "\n";
    }
}
