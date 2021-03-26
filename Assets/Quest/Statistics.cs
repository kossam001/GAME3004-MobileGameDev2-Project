using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Statistics", menuName = "Quests/Statistics")]
public class Statistics : ScriptableObject
{
    public int id;
    public int progress;

    public delegate void ProgressUpdated(int progressMade);
    public ProgressUpdated OnProgressUpdated;

    public void UpdateProgress(int progressMade)
    {
        progress += progressMade;
        OnProgressUpdated(progress);
    }
}
