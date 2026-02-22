using UnityEngine;

public class DoorTestHotspot : HSInteract {

    [SerializeField]
    GameObject doorObject;

    protected override void OnInteract(ItemData item) {
        doorObject.SetActive(true);
    }
}