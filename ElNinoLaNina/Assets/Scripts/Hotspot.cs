using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;

public class Hotspot : MonoBehaviour {
    public enum HotspotType { Navigate, Interact, Examine }
    public HotspotType type;

    [Tooltip("Target viewpoint (only used if Navigate)")]
    public Viewpoint targetView;

    [Tooltip("Only active if the player is in one of these viewpoints")]
    public List<Viewpoint> activeInViews;

    private Collider col;
    private CameraController cam;

    void Awake() {
        col = GetComponent<Collider>();
        cam = FindFirstObjectByType<CameraController>();
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

        // Navigation
        if (type == HotspotType.Navigate && targetView != null) {
            cam.GoTo(targetView);
        }

        // TODO: Interact / Examine
    }

}

