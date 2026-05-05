using UnityEngine;

public class HSCollect : HSInteract
{
    [SerializeField]
    private GameObject collectibleItem;
    [SerializeField]
    private ItemData collectibleItemData;

    void Start() {
        UpdateFromState();
        UpdateCollider(); // NOTE: This somehow fixes colliders
    }

    public override void OnInteract(ItemData item) {
        InventoryManager.Instance.AddItem(collectibleItemData);
        state = 1;
        UpdateFromState();
    }

    private void UpdateFromState() {
        if (state == 1) {
            EnableCollider(false);
            collectibleItem.SetActive(false);
        } else if (state == 0) {
            EnableCollider(true);
        } else {
            throw new System.Exception(name + " in Invalid State.");
        }
    }

    public override void OnHoverEnterTrigger() {
        SetCollectibleItemLayer(8);
        base.OnHoverEnterTrigger();
    }

    public override void OnHoverExitTrigger() {
        SetCollectibleItemLayer(0);
        base.OnHoverExitTrigger();
    }

    void SetCollectibleItemLayer(int layer) {
        collectibleItem.layer = layer;
        foreach (Transform child in collectibleItem.transform) {
            child.gameObject.layer = layer;
        }
    }
}
