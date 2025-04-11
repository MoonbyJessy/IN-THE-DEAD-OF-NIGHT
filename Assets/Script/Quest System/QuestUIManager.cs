using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class QuestUIManager : MonoBehaviour
{
    public GameObject questPanel;
    public GameObject questEntryPrefab;   
    public GameObject objectiveEntryPrefab;

    private Dictionary<Quest, GameObject> questToUIMap = new Dictionary<Quest, GameObject>();

    private void Start()
    {
        foreach (var quest in QuestManager.Instance.activeQuests)
        {
            AddQuestToUI(quest);
        }
        QuestManager.Instance.OnObjectiveUpdated += UpdateObjectiveUI;
    }

    public void AddQuestToUI(Quest quest)
    {
        if (questToUIMap.ContainsKey(quest)) return;

        GameObject questUI = Instantiate(questEntryPrefab, questPanel.transform);
        questUI.transform.Find("QuestName").GetComponent<TextMeshProUGUI>().text = quest.questName;

        foreach (var obj in quest.objectives)
        {
            GameObject objEntry = Instantiate(objectiveEntryPrefab, questPanel.transform);
            objEntry.GetComponent<TextMeshProUGUI>().text = obj.description;
            objEntry.name = obj.objectiveID;
        }

        questToUIMap.Add(quest, questUI);
    }

    public void UpdateObjectiveUI(string objectiveID)
    {
        foreach (var pair in questToUIMap)
        {
            Quest quest = pair.Key;
            foreach (var obj in quest.objectives)
            {
                if (obj.objectiveID == objectiveID && obj.isComplete)
                {
                    Transform objEntry = questPanel.transform.Find(obj.objectiveID);
                    if (objEntry != null)
                    {
                        var text = objEntry.GetComponent<TextMeshProUGUI>();
                        text.text = $"<s>{obj.description}</s>";
                        text.color = Color.gray;
                    }
                    return;
                }
            }
        }
    }
}

