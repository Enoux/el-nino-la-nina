using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private List<ItemData> items = new List<ItemData>();
    public ItemData SelectedItem { get; private set; }

    public event Action OnInventoryChanged;
    public event Action<ItemData> OnSelectionChanged;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void AddItem(ItemData item) {
        if (!items.Contains(item)) {
            items.Add(item);
            OnInventoryChanged?.Invoke();
        }
    }

    public void RemoveItem(ItemData item)
    {
        if (items.Remove(item))
        {
            if (SelectedItem == item)
                ClearSelection();

            OnInventoryChanged?.Invoke();
        }
    }

    public List<ItemData> GetItems()
    {
        return items;
    }

    public void SelectItem(ItemData item)
    {
        SelectedItem = item;
        OnSelectionChanged?.Invoke(item);
    }

    public void ClearSelection()
    {
        SelectedItem = null;
        OnSelectionChanged?.Invoke(null);
    }

}
