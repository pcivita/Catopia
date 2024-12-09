using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        if (dialogue == null)
        {
            Debug.LogError("Dialogue is not assigned in the Inspector!");
            return;
        }
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
