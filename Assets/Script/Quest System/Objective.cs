using UnityEngine;
using System;

[Serializable]
public class Objective
{
    public string objectiveID;
    public string description;
    public bool isComplete;

    public Action OnObjectiveComplete;

    public void CompleteObjective()
    {
        if (!isComplete)
        {
            isComplete = true;
            OnObjectiveComplete?.Invoke();
            Debug.Log($"Objective Completed: {description}");
        }
    }
}
