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

    private void UpdateProgress(int progressMade)
    {
        progress += progressMade;

        if (OnProgressUpdated == null) return;

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
