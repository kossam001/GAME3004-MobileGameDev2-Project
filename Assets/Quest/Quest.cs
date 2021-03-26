using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "Quest", menuName = "Quests/Quest")]
public class Quest : ScriptableObject
{
    public int id;
    public string questName;
    public string objective;
    public int progress;
    public int completionAmount;
    public bool completed;

    public GameObject questObject;

    public void UpdateProgress(int progressMade)
    {
        if (progress == completionAmount) return;

        progress += progressMade;
        progress = Mathf.Clamp(progress, 0, completionAmount);
    }

    public void SetDisplay(Quest quest)
    {
        TMP_Text questText = questObject.GetComponentInChildren<TMP_Text>();

        questText.text = quest.name + "\n";
        questText.text += quest.objective + " ";
        questText.text += "Progress: " + quest.progress + " / " + quest.completionAmount + "\n";
    }
}
