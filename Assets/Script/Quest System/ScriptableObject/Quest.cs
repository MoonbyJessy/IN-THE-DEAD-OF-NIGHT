using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest System/Quest")]
public class Quest : ScriptableObject
{
    public string questName;
    public string description;
    public List<Objective> objectives = new List<Objective>();

    public bool IsCompleted()
    {
        foreach (var obj in objectives)
        {
            if (!obj.isComplete)
                return false;
        }
        return true;
    }
}
