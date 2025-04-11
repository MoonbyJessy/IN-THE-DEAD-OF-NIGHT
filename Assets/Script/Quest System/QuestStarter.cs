using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStarter : MonoBehaviour
{
    public Quest myQuest;

    void Start()
    {
        QuestManager.Instance.AddQuest(myQuest);
        FindObjectOfType<QuestUIManager>().AddQuestToUI(myQuest);
    }

}
