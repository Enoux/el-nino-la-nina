using UnityEngine;
using System.Collections.Generic;

public class FatherCH : Character
{
    [SerializeField]
    private ItemData basementKey;

    protected override void OnGiveItem(ItemData item)
    {
        Debug.Log("Yo father");
    }

    protected override void Talk(string scenario)
    {
        // Use DialogueManager.Instance to access dialogue manager in the Scene
        switch (scenario) {
            case "Talk":
                // Give key
                if (state != 1)
                {
                    DialogueManager.Instance.StartDialogue(base.characterDialogues[0]);
                    InventoryManager.Instance.AddItem(basementKey);
                    state = 1;
                }

                else
                {
                    DialogueManager.Instance.StartDialogue(base.characterDialogues[1]);
                }
                break;
            default:
                break;
        }
    }
}