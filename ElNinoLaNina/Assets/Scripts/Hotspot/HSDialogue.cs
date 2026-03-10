using UnityEngine;

public class HSDialogue : Hotspot, IClickReceiver
{
    public DialogueData dialogue;

    public void ActivateDialogue() {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}