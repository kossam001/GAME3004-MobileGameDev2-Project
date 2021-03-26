using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestLog", menuName = "Quests/QuestLog")]
public class QuestLog : ScriptableObject
{
    public List<Quest> allQuests;

    public Quest getQuest(int id)
    {
        return allQuests[id];
    }
}
