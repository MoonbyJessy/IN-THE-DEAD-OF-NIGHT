using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Narration/Dialogue/Dialogue")]
public class Dialogue : ScriptableObject
{
    [SerializeField]
    private DialogueNode m_FirstNode;

    public DialogueNode FirstNode => m_FirstNode;
}
