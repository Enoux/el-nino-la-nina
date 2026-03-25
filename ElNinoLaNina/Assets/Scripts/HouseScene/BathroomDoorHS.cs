using UnityEngine;

public class BathroomDoorHS : HSInteract {

    [SerializeField]
    public Animator doorAnim;
    public ItemData bathroomKey;
    private int doorState = 0;
    private BoxCollider bathroomDoor;

    protected override void OnInteract(ItemData item = null) {
        
        if (item == bathroomKey)
        {
            doorState = 1;
            Debug.Log("The door is now unlocked!");
        }

        else if (doorState == 1)
        {
            doorAnim.SetInteger("state", doorState);
            bathroomDoor = GetComponent<BoxCollider>();
            bathroomDoor.enabled = false; 
        }

        else if (state == 0)
        {
            Debug.Log("The door is locked!");
        }
    }
}