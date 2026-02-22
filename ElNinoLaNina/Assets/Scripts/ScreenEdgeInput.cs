using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class ScreenEdgeInput : MonoBehaviour {
    [Header("References")]
    public CameraController cameraController;
    public Camera mainCamera;

    [Header("Edge Sizes (percentage of screen)")]
    [Range(0.01f, 0.3f)] public float leftEdge = 0.08f;
    [Range(0.01f, 0.3f)] public float rightEdge = 0.08f;
    [Range(0.01f, 0.3f)] public float bottomEdge = 0.12f;
    [Range(0.01f, 0.3f)] public float topEdge = 0.08f;
    private enum EdgeType {left, right, bottom, top, none};

    [Header("Raycast")]
    public LayerMask hotspotLayer;

    [Header("Cursor Sprites")]
    public Texture2D defaultCursor;
    public Texture2D pressCursor;
    public Texture2D takeItemCursor;
    public Texture2D useItemCursor;
    public Texture2D useItemValidCursor;
    public Texture2D leftCursor;
    public Texture2D rightCursor;
    public Texture2D bottomCursor;
    public Texture2D topCursor;

    Hotspot currentHover;

    void Update() {
        if (!cameraController || !cameraController.CanNavigate()) {
            return;
        } else if (EventSystem.current.IsPointerOverGameObject()) {
            return; // Is in the Inventory/Pause Menu
        }

        // Changing the Current Hover
        var hitHotspot = TryRaycastHotspot();
        if (hitHotspot != currentHover) {
            if (currentHover != null) {
                currentHover.OnHoverExit();
            }

            currentHover = hitHotspot;
            if (currentHover != null) {
                currentHover.OnHoverEnter();
            }
        }

        // Cursor Handling if Item is Selected
        if (InventoryManager.Instance.SelectedItem != null) {
            if (currentHover != null && currentHover.type == Hotspot.HotspotType.Interact) {
                SetCursor(useItemValidCursor);
                if (Mouse.current.leftButton.wasPressedThisFrame) {
                    currentHover.Activate();
                }
                
            } else {
                SetCursor(useItemCursor);
            }

            if (Mouse.current.leftButton.wasPressedThisFrame) {
                InventoryManager.Instance.ClearSelection();
            }

        // Cursor Handling for Navigation Mode
        } else {
            if (currentHover == null) {
                TrySetEdgeCursor();
            } else {
                SetCursor(pressCursor);
            }

            if (Mouse.current.leftButton.wasPressedThisFrame) {
                if (currentHover == null) {
                    TryEdgeNavigation();
                } else {
                    currentHover.Activate();
                } 
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

    EdgeType getEdgeType() {
        Vector2 m = Mouse.current.position.ReadValue();
        Viewpoint v = cameraController.currentView;

        if (m.x < Screen.width * leftEdge && v.left) {
            return EdgeType.left;
        } else if (m.x > Screen.width * (1f - rightEdge) && v.right) {
            return EdgeType.right;
        } else if (m.y < Screen.height * bottomEdge && v.down) {
            return EdgeType.bottom;
        } else if (m.y > Screen.height * (1f - topEdge) && v.up) {
            return EdgeType.top;
        } else {
            return EdgeType.none;
        }
    }

    void TryEdgeNavigation() {
        Viewpoint v = cameraController.currentView;

        switch(getEdgeType()) {
            case EdgeType.left: cameraController.GoTo(v.left); break;
            case EdgeType.right: cameraController.GoTo(v.right); break;
            case EdgeType.bottom: cameraController.GoTo(v.down); break;
            case EdgeType.top: cameraController.GoTo(v.up); break;
            default: break;
        }
    }

    void TrySetEdgeCursor() {
        switch(getEdgeType()) {
            case EdgeType.left: SetCursor(leftCursor); break;
            case EdgeType.right: SetCursor(rightCursor); break;
            case EdgeType.bottom: SetCursor(bottomCursor); break;
            case EdgeType.top: SetCursor(topCursor); break;
            default: SetCursor(defaultCursor); break;
        }
    }

    void SetCursor(Texture2D texture) {
        Vector2 hotspotOffset = new Vector2(0,0);
        Cursor.SetCursor(texture, hotspotOffset, CursorMode.Auto);
    }
}
