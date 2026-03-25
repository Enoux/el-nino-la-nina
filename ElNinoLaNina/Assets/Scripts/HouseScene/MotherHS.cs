using UnityEngine;

public class MotherHS : HSInteract
{
    public Character mother;
    public HouseLevelManager levelManager;

    protected override void OnInteract(ItemData item = null) {
        mother.Interact(item);
        Debug.Log("Hello mother");
    }

    public int getState() {
        return mother.GetState();
    }
}
