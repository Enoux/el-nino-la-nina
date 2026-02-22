using UnityEngine;

public class HSNavigate : Hotspot {

    [Tooltip("Target viewpoint (only used if Navigate)")]
    public Viewpoint targetView;

    // Called by your input system when clicked
    public void ActivateGoTo() {
        Debug.Assert(col.enabled);
        Debug.Assert(targetView);
        cam.GoTo(targetView);
        OnClick();
    }
}