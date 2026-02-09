using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;
using System;

public class Hotspot : MonoBehaviour {
    public enum HotspotType { Navigate, Interact, Examine }
    public HotspotType type;

    [Tooltip("Target viewpoint (only used if Navigate)")]
    public Viewpoint targetView;

    [Tooltip("Only active if the player is in one of these viewpoints")]
    public List<Viewpoint> activeInViews;

    [Tooltip("Alerted when Hotspot is hovered.")]
    public List<MonoBehaviour> hoverTargets;

    [Tooltip("Alerted when Hotspot is clicked.")]
    public List<MonoBehaviour> clickTargets;

    private Collider col;
    private CameraController cam;
    private List<IHoverReceiver> hoverReceivers = new();
    private List<IClickReceiver> clickReceivers = new();


    void Awake() {
        col = GetComponent<Collider>();
        cam = FindFirstObjectByType<CameraController>();

        foreach (var target in hoverTargets) {
            hoverReceivers.Add((IHoverReceiver)target);
        }
        foreach (var target in clickTargets) {
            clickReceivers.Add((IClickReceiver)target);
        }

        UpdateCollider();
    }

    // Call this whenever the viewpoint changes
    public void UpdateCollider() {
        // Enable collider only if current view is in the active list
        if (activeInViews.Count > 0) {
            col.enabled = activeInViews.Contains(cam.currentView);
        } else {
            col.enabled = true; // no restriction means always active
        }
    }

    // Called when clicked
    public void Activate() {
        Debug.Assert(col.enabled);
        OnClick();

        // Navigation
        if (type == HotspotType.Navigate && targetView != null) {
            cam.GoTo(targetView);
        }

        // TODO: Interact / Examine
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
