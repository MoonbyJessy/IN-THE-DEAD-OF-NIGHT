using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueException : System.Exception
{
    
    public DialogueException(string message) : base(message)
    { }

}

public class DialogueSeqencer
{
    public delegate void DialogueCallback(Dialogue dialogue);
    public delegate void DialogueNodeCallback(Dialogue node);

    public DialogueCallback OnDialogueStart;
    public DialogueCallback OnDialogueEnd;
    public DialogueNodeCallback OnDialogueNodeStart;
    public DialogueNodeCallback OnDialogueNodeEnd;

    private Dialogue m_CurrentDialogue;
    private DialogueNode m_currentNode;

    public void StartDialogue(Dialogue dialogue)
    {
        if (m_CurrentDialogue == null)
        {
            m_CurrentDialogue = dialogue;
            OnDialogueStart?.Invoke(m_CurrentDialogue);
            StartDialogueNode(dialogue.FirstNode);

        }
        else
        {
            throw new DialogueException("can't start a dialogue when another is already running.");
        }
    }
    public void EndDialogue(Dialogue dialogue)
    {
        if (m_CurrentDialogue == dialogue)
        {
            StopDialogueNode(m_currentNode);
            OnDialogueEnd?.Invoke(m_CurrentDialogue);
            m_CurrentDialogue = null;
        }
        else
        {
            throw new DialogueException("Trying to stop a dialogue that isn't running.");

        }
    }

    private bool CanStartNode(DialogueNode node)
    {
        return (m_currentNode == null || node == null || m_currentNode.CanBeFollowedByNode(node));

    }

    public void StartDialogueNode(DialogueNode node)
    {
        if (CanStartNode(node))
        {
            StopDialogueNode(m_currentNode);

            m_currentNode = node;

            if (m_currentNode != null)
            {
           //     OnDialogueNodeStart?.Invoke(m_currentNode);

            }
            else
            {
                EndDialogue(m_CurrentDialogue);
            }
        }
        else
        {
            throw new DialogueException("Failed to start dialogue node");
        }
    }
    private void StopDialogueNode(DialogueNode node)
    {
        if (m_currentNode == node)
        {
         //   OnDialogueNodeEnd?.Invoke(m_currentNode);
            m_currentNode = null;
        }
    }
}