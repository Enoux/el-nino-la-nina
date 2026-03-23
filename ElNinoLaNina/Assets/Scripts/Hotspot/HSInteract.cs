using UnityEngine;
using System.Collections.Generic;

abstract public class HSInteract : Hotspot
{
    [Tooltip("Items that can be used on this hotspot")]
    [SerializeField]
    private List<ItemData> acceptedItems = new();

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
}
