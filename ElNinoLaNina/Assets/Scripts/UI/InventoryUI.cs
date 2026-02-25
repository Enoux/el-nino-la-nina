using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour {
    public GameObject itemButtonPrefab;
    public Transform contentRoot;

    private List<ItemButton> itemButtons;

    private void Start() {
        InventoryManager.Instance.OnInventoryChanged += Refresh;
        InventoryManager.Instance.OnSelectionChanged += UpdateSelectionVisual;
        Refresh();
    }


    private void OnDisable() {
        InventoryManager.Instance.OnInventoryChanged -= Refresh;
        InventoryManager.Instance.OnSelectionChanged -= UpdateSelectionVisual;
    }

    void Refresh() {
        foreach (Transform child in contentRoot) {
            Destroy(child.gameObject);
        }

        itemButtons = new();
        foreach (var item in InventoryManager.Instance.GetItems()) {
            var go = Instantiate(itemButtonPrefab, contentRoot);
            var itemButton = go.GetComponent<ItemButton>();
            itemButton.Initialize(item);
            itemButtons.Add(itemButton);
        }
    }

    void UpdateSelectionVisual(ItemData selected) {
        foreach (var itemButton in itemButtons) {
            itemButton.SetOutline(
                itemButton.Item == InventoryManager.Instance.SelectedItem);
        }
    }
}