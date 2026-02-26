using UnityEngine;

public class R1_DoorHS : HSInteract {

    [SerializeField]
    GameObject doorObject;
    private int state = 0;
    public TutorialLevelManager levelManager;
    public ItemData key;

    protected override void OnInteract(ItemData item = null) {
        if (state == 0)
        {
            if (item == key)
            {
                state = 1;
                Debug.Log("Door unlocked");
            }
            else
            {
                Debug.Log("Door is locked");
            }
            
        }
        else if (state == 1)
        {
            levelManager.AttemptWin();
        }
        
    }
}