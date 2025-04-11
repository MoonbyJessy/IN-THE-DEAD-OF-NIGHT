using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    public string objectiveID;
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            QuestManager.Instance.CompleteObjective(objectiveID);
            triggered = true;
        }
    }
}
