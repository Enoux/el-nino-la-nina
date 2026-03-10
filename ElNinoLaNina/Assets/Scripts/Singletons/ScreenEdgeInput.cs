using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class ScreenEdgeInput : MonoBehaviour {
    public static ScreenEdgeInput Instance;

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
    public Texture2D craftCursor;
    public Texture2D leftCursor;
    public Texture2D rightCursor;
    public Texture2D bottomCursor;
    public Texture2D topCursor;

    Hotspot currentHover;
    private ItemData hoveredInventoryItem;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Update() {
        if (!cameraController || !cameraController.CanNavigate()) {
            return;
        } 

        // Handle inventory hover BEFORE UI early return
        if (hoveredInventoryItem != null) {
            if (InventoryManager.Instance.SelectedItem != null) {
                SetCursor(craftCursor);
            } else {
                SetCursor(pressCursor);
            }
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        var hasSelectedItem = InventoryManager.Instance.SelectedItem != null;
        var clicked = Mouse.current.leftButton.wasPressedThisFrame;

        // Changing the Current Hover
        var hitHotspot = TryRaycastHotspot();
        if (hitHotspot is HSNavigate) { 
            hitHotspot = hasSelectedItem? null: hitHotspot;
        }

        if (hitHotspot != currentHover) {
            if (currentHover != null) {
                currentHover.OnHoverExit();
            }

            currentHover = hitHotspot;
            if (currentHover != null) {
                currentHover.OnHoverEnter();
            }
        }

        // Cursor Sprite Handling
        if (hasSelectedItem) {
            SetCursor(currentHover is HSInteract ? useItemValidCursor : useItemCursor);
        } else if (currentHover == null) {
            TrySetEdgeCursor();
        } else {
            SetCursor(pressCursor);
        }

        // Cursor Click Handling
        if (clicked && currentHover is HSNavigate navigateHover) {
            // NOTE: currentHover is null when hasSelectedItem, so no need to check again
            navigateHover.ActivateGoTo(); 
        } else if (clicked && currentHover is HSInteract interactHover) {
            var item = InventoryManager.Instance.SelectedItem;
            interactHover.ActivateInteract(item);
        }
        if (clicked && hasSelectedItem) {
            InventoryManager.Instance.ClearSelection();
        } else if (clicked && !hasSelectedItem) {
            TryEdgeNavigation();
        }


        // Debug Stuff
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

    public void OnInventoryHover(ItemData item) {
        hoveredInventoryItem = item;
    }

    public void OnInventoryExit() {
        hoveredInventoryItem = null;
    }
}
