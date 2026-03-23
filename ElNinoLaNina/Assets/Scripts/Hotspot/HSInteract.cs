using UnityEngine;
using System.Collections.Generic;

abstract public class HSInteract : Hotspot
{
    [Tooltip("Items that can be used on this hotspot")]
    [SerializeField]
    private List<ItemData> acceptedItems = new();
    protected int state = 0;
    protected int hotspotIdx;

    new void Awake()
    {

        // Run hotspot Awake()
        base.Awake();
        
        List<string> hotspotStates = PlayerSaveFile.currentSaveFile.hotspotStateKeys;
        string name = this.gameObject.name;

        // GameObject already in hotspotStates
        if (hotspotStates.Contains(name))
        {   
            hotspotIdx = hotspotStates.IndexOf(name);

            // Item already collected; should not exist anymore
            state = PlayerSaveFile.currentSaveFile.hotspotStateVals[hotspotIdx];
        }

        else
        {
            PlayerSaveFile.currentSaveFile.hotspotStateKeys.Add(name);
            PlayerSaveFile.currentSaveFile.hotspotStateVals.Add(0);
            hotspotIdx = hotspotStates.Count - 1;
        }
    }

    public virtual bool CanActivateInteract(ItemData item) {
        return acceptedItems.Contains(item);
    }

    public void ActivateInteract(ItemData item) {
        if (!CanActivateInteract(item)) {
            Debug.Log("Wrong item.");
            return;
        }
        
        if (item != null && item.consumeOnUse) {
            InventoryManager.Instance.RemoveItem(item);
        }
        OnInteract(item);
    }

    protected virtual void OnInteract(ItemData item) {
        // Override in derived hotspot scripts
    }

    protected void UpdateState()
    {
        PlayerSaveFile.currentSaveFile.hotspotStateVals[hotspotIdx] = state;
    }
}
