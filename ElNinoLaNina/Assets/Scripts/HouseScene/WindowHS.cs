using UnityEngine;

public class WindowHS : HSInteract {

    [SerializeField]
    public HouseLevelManager levelManager;
    public ItemData fork;

    public override void OnInteract(ItemData item = null) {
        if (state == 0)
        {
            if (item == fork)
            {
                state = 1;
            }
            else
            {
                Debug.Log("Window is sealed");
            }
            
        }
        else if (state == 1)
        {
            levelManager.AttemptExitWindow();
        }
    }
}