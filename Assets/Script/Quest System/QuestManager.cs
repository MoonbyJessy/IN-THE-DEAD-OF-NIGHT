using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public List<Quest> activeQuests = new List<Quest>();
    public event Action<string> OnObjectiveUpdated;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddQuest(Quest quest)
    {
        if (!activeQuests.Contains(quest))
        {
            activeQuests.Add(quest);
            Debug.Log($"Quest Added: {quest.questName}");
        }
    }

    public void CompleteObjective(string objectiveID)
    {
        foreach (var quest in activeQuests)
        {
            foreach (var objective in quest.objectives)
            {
                if (objective.objectiveID == objectiveID && !objective.isComplete)
                {
                    objective.CompleteObjective();
                    OnObjectiveUpdated?.Invoke(objectiveID);
                    return;
                }
            }
        }
    }
}
