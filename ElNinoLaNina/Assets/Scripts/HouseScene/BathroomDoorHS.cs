using UnityEngine;

public class BathroomDoorHS : HSInteract {

    [SerializeField]
    public Animator doorAnim;
    public ItemData bathroomKey;
    public Hotspot navBathroomDoor;

    void Start()
    {
        navBathroomDoor.EnableCollider(false);
    }

  protected override void OnInteract(ItemData item = null) {
        
        if (state == 0 && item == bathroomKey)
        {
            state = 1;
            UpdateState();
            Debug.Log("The door is now unlocked!");
        }

        else if (state == 1)
        {
            doorAnim.SetInteger("state", state);
            EnableCollider(false);
            navBathroomDoor.EnableCollider(true);
        }

        else if (state == 0)
        {
            Debug.Log("The door is locked!");
        }
    }
}