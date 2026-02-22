using UnityEngine;

public class DoorTestHotspot : Hotspot {

    [SerializeField]
    GameObject doorObject;

    protected override void OnInteract(ItemData item) {
        doorObject.SetActive(true);
    }
}