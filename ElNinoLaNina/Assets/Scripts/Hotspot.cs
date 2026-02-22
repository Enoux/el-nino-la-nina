using UnityEngine;
using System.Collections.Generic;
using System;

public class Hotspot : MonoBehaviour {

    public enum HotspotType { Navigate, Interact, Collect }
    public HotspotType type;

    [Tooltip("Target viewpoint (only used if Navigate)")]
    public Viewpoint targetView;

    [Tooltip("Only active if the player is in one of these viewpoints")]
    public List<Viewpoint> activeInViews;

    [Tooltip("Alerted when Hotspot is hovered.")]
    public List<MonoBehaviour> hoverTargets;

    [Tooltip("Alerted when Hotspot is clicked.")]
    public List<MonoBehaviour> clickTargets;

    [Tooltip("Items that can be used on this hotspot")]
    [SerializeField]
    private List<ItemData> acceptedItems = new();

    private Collider col;
    private CameraController cam;

    private List<IHoverReceiver> hoverReceivers = new();
    private List<IClickReceiver> clickReceivers = new();

    void Awake() {
        col = GetComponent<Collider>();
        cam = FindFirstObjectByType<CameraController>();

        foreach (var target in hoverTargets) {
            if (target is IHoverReceiver receiver) {
                hoverReceivers.Add(receiver);
            }
        }

        foreach (var target in clickTargets) {
            if (target is IClickReceiver receiver) {
                clickReceivers.Add(receiver);
            }
        }

        UpdateCollider();
    }

    // Call this whenever the viewpoint changes
    public void UpdateCollider() {
        if (activeInViews.Count > 0) {
            col.enabled = activeInViews.Contains(cam.currentView);
        } else {
            col.enabled = true;
        }
    }

    // Called by your input system when clicked
    public void Activate() {
        Debug.Assert(col.enabled);
        OnClick();

        switch (type) {
            case HotspotType.Navigate:
                if (targetView != null) {
                    cam.GoTo(targetView);
                }
                break;
            case HotspotType.Interact:
                var selectedItem = InventoryManager.Instance.SelectedItem;
                Interact(selectedItem);
                break;
            case HotspotType.Collect: 
                break;
        }
    }

    private void Interact(ItemData item) {
        if (acceptedItems.Contains(item)) {
            if (item != null && item.consumeOnUse) {
                InventoryManager.Instance.RemoveItem(item);
            }
            OnInteract(item);
        } else {
            Debug.Log("Wrong item.");
        }
    }

    protected virtual void OnInteract(ItemData item) {
        // Override in derived hotspot scripts
        // Example:
        // - Open door
        // - Activate mechanism
        // - Reveal object
        // - Change viewpoint
    }

    public void OnHoverEnter() {
        foreach (var r in hoverReceivers) {
            r.OnHoverEnter();
        }
    }

    public void OnHoverExit() {
        foreach (var r in hoverReceivers) {
            r.OnHoverExit();
        }
    }

    public void OnClick() {
        foreach (var r in clickReceivers) {
            r.OnClick();
        }
    }
}

public interface IHoverReceiver {
    void OnHoverEnter();
    void OnHoverExit();
}

public interface IClickReceiver {
    void OnClick();
}
