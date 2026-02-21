using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ScreenEdgeInput : MonoBehaviour {
    [Header("References")]
    public CameraController cameraController;
    public Camera mainCamera;

    [Header("Edge Sizes (percentage of screen)")]
    [Range(0.01f, 0.3f)] public float leftEdge = 0.08f;
    [Range(0.01f, 0.3f)] public float rightEdge = 0.08f;
    [Range(0.01f, 0.3f)] public float bottomEdge = 0.12f;
    [Range(0.01f, 0.3f)] public float topEdge = 0.08f;

    [Header("Raycast")]
    public LayerMask hotspotLayer;

    Hotspot currentHover;

    void Update() {
        if (!cameraController || !cameraController.CanNavigate())
            return;

        var hitHotspot = TryRaycastHotspot();

        // Hover Handling (do only if different)
        if (hitHotspot != currentHover) {
            if (currentHover != null) {
                currentHover.OnHoverExit();
            }

            currentHover = hitHotspot;
            if (currentHover != null) {
                currentHover.OnHoverEnter();
            }
        }

        // Click Handling (Try Hotspot over Edge Nav)
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            if (hitHotspot != null) {
                hitHotspot.Activate();
            } else {
                TryEdgeNavigation();
            } 
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame) {
            Debug.Log("Escape pressed!");
            SceneManager.LoadSceneAsync("MainMenu");
        }

    }

    Hotspot TryRaycastHotspot() {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, hotspotLayer)) {
            return hit.collider.GetComponent<Hotspot>();
        }
        return null;
    }

    void TryEdgeNavigation() {
        Vector2 m = Mouse.current.position.ReadValue();
        Viewpoint v = cameraController.currentView;

        if (m.x < Screen.width * leftEdge && v.left) {
            cameraController.GoTo(v.left);
        }
        else if (m.x > Screen.width * (1f - rightEdge) && v.right) {
            cameraController.GoTo(v.right);
        }

        else if (m.y < Screen.height * bottomEdge && v.down) {
            cameraController.GoTo(v.down);
        }

        else if (m.y > Screen.height * (1f - topEdge) && v.up) {
            cameraController.GoTo(v.up);
        }
    }
}
