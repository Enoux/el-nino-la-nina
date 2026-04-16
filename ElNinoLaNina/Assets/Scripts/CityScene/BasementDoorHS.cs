using UnityEngine;

public class BasementDoorHS : HSInteract {

    [SerializeField]
    public Animator doorAnim;
    public ItemData basementKey;
    public Hotspot navBasementDoor;

    void Start()
    {
        navBasementDoor.EnableCollider(false);
    }

    public override void OnInteract(ItemData item = null) {
        
        if (state == 0 && item == basementKey)
        {
            InventoryManager.Instance.RemoveItem(basementKey);
            state = 1;
            Debug.Log("The door is now unlocked!");
        }

        else if (state == 1)
        {
            doorAnim.SetInteger("state", state);
            EnableCollider(false);
            navBasementDoor.EnableCollider(true);
        }

        else if (state == 0)
        {
            Debug.Log("The door is locked!");
        }
    }
}