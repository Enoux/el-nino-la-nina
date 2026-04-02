using UnityEngine;

public class HSDialogue : Hotspot
{
    public DialogueData dialogue;

    public void ActivateDialogue() {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}