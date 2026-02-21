using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {
    public GameObject itemButtonPrefab;
    public Transform contentRoot;

    private void OnEnable() {
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

        foreach (var item in InventoryManager.Instance.GetItems()) {
            var go = Instantiate(itemButtonPrefab, contentRoot);
            var button = go.GetComponent<Button>();
            var image = go.GetComponent<Image>();

            image.sprite = item.icon;

            button.onClick.AddListener(() => {
                InventoryManager.Instance.SelectItem(item);
            });
        }
    }

    void UpdateSelectionVisual(ItemData selected) {
        // Optional: highlight selected button
        // You can improve this later
    }
}