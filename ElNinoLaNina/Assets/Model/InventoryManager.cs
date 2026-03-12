using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<ItemData> items = new List<ItemData>();
    public int inventorySize = 5;
    public ItemData SelectedItem { get; private set; }

    public event Action OnInventoryChanged;
    public event Action<ItemData> OnSelectionChanged;
    private CraftingRecipe[] recipes;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            InventoryManager.Instance.items = PlayerSaveFile.currentSaveFile.playerItems;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        recipes = Resources.LoadAll<CraftingRecipe>("CraftingRecipe");
    }

    public void AddItem(ItemData item) {
        if (!items.Contains(item)) {
            Debug.Log("Added");
            items.Add(item);
            OnInventoryChanged?.Invoke();
        }
    }

    public void RemoveItem(ItemData item) {
        if (items.Remove(item)) {
            if (SelectedItem == item) {
                ClearSelection();
            }

            OnInventoryChanged?.Invoke();
        }
    }

    public List<ItemData> GetItems() {
        return items;
    }

    public void SelectItem(ItemData item) {
        SelectedItem = item;
        OnSelectionChanged?.Invoke(item);
    }

    public void ClearSelection() {
        SelectedItem = null;
        OnSelectionChanged?.Invoke(null);
    }

    public bool TryCraft(ItemData a, ItemData b) {
        foreach (var recipe in recipes) {
            bool match =
                (recipe.itemA == a && recipe.itemB == b) ||
                (recipe.itemA == b && recipe.itemB == a);

            if (match) {
                if (recipe.consumeA) {RemoveItem(recipe.itemA);}
                if (recipe.consumeB) {RemoveItem(recipe.itemB);}

                AddItem(recipe.result);
                ClearSelection();
                Debug.Log("Crafted " + recipe.result.itemName);

                return true;
            }
        }

        Debug.Log("No recipe found");
        return false;
    }

}
