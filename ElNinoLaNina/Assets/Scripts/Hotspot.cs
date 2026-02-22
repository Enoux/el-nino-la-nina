using UnityEngine;
using System.Collections.Generic;
using System;

public class Hotspot : MonoBehaviour {

    [Tooltip("Only active if the player is in one of these viewpoints")]
    public List<Viewpoint> activeInViews;

    [Tooltip("Alerted when Hotspot is hovered.")]
    public List<MonoBehaviour> hoverTargets;

    [Tooltip("Alerted when Hotspot is clicked.")]
    public List<MonoBehaviour> clickTargets;

    protected Collider col;
    protected CameraController cam;
    protected List<IHoverReceiver> hoverReceivers = new();
    protected List<IClickReceiver> clickReceivers = new();

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
