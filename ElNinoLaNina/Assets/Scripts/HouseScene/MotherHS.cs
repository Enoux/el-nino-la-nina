using UnityEngine;

public class MotherHS : HSInteract
{
    public Character mother;
    public HouseLevelManager levelManager;

    public override void OnInteract(ItemData item) {
        mother.Interact(item);
        Debug.Log("Hello mother");
    }

    public int getState() {
        return mother.GetState();
    }
}
