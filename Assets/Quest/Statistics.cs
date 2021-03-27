using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Statistics", menuName = "Quests/Statistics")]
public class Statistics : ScriptableObject
{
    public int id;
    [SerializeField] private int progress;

    public delegate void ProgressUpdated(int progressMade);
    public ProgressUpdated OnProgressUpdated;

    public void UpdateProgress(int progressMade)
    {
        progress += progressMade;
        OnProgressUpdated(progress);
    }

    public void SetProgress(int desiredProgress)
    {
        progress = desiredProgress;

        if (OnProgressUpdated == null) return;
        
        OnProgressUpdated(progress);
    }

    public int GetProgress()
    {
        return progress;
    }
}
