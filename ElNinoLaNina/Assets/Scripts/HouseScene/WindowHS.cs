using UnityEngine;

public class WindowHS : HSInteract {

    [SerializeField]
    public HouseLevelManager levelManager;
    public ItemData fork;

    protected override void OnInteract(ItemData item = null) {
        if (base.state == 0)
        {
            if (item == fork)
            {
                base.state = 1;
            }
            else
            {
                Debug.Log("Window is sealed");
            }
            
        }
        else if (base.state == 1)
        {
            levelManager.AttemptExitWindow();
        }
    }
}