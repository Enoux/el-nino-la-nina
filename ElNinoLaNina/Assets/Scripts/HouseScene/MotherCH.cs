using UnityEngine;
using System.Collections.Generic;

public class MotherCH : Character
{
    [SerializeField]
    private ItemData goBag;

    protected override void OnGiveItem(ItemData item)
    {
        if (item == goBag) {
            Debug.Log("Gave Go Bag");
            
            InventoryManager.Instance.RemoveItem(goBag);
            state = 1;
            Talk("Healed");
        }
    }

    protected override void Talk(string scenario)
    {
        // Use DialogueManager.Instance to access dialogue manager in the Scene
        switch (scenario) {
            case "Talk":
                if (scenario == "Talk" && state == 0) {
                    DialogueManager.Instance.StartDialogue(base.characterDialogues[0]);
                }
                break;
            case "Healed":
                DialogueManager.Instance.StartDialogue(base.characterDialogues[1]);
                break;
            default:
                break;
        }
    }
}