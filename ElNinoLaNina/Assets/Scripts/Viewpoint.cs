using UnityEngine;

public class Viewpoint : MonoBehaviour {
    [Header("Navigation Links")]
    public Viewpoint left;
    public Viewpoint right;
    public Viewpoint up;
    public Viewpoint down;

    [Header("Optional")]
    [Tooltip("If false, navigation into this view is blocked")]
    public bool isActive = true;
}
