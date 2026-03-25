using UnityEngine;

public class MotherHS : HSInteract
{
    public Character mother;
    public HouseLevelManager levelManager;
    public ItemData goBag;

    protected override void OnInteract(ItemData item = null) {
        if (base.state == 0)
        {
            if (item == goBag)
            {
                base.state = 1;
            }
            else
            {
                Debug.Log("Mother is injured and cannot move. Go find the Go Bag");
            }
            
        }
    }

    public int getState() {
        return base.state;
    }
}
