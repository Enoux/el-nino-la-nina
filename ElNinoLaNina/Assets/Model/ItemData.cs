using UnityEngine;

[CreateAssetMenu(menuName = "CustomObjects/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    [TextArea] public string description;

    public GameObject previewPrefab;
    public Sprite icon;
    public Sprite hoverIcon;

    public Vector3 previewRotation;
    public float previewScale = 1f;
    // public Vector3 previewOffset;
}