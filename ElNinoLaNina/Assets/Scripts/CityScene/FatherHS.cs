using UnityEngine;

public class FatherHS : HSInteract
{
    public Character father;
    public CityLevelManager levelManager;

    public override void OnInteract(ItemData item) {
        father.Interact(item);
        Debug.Log("Hello father");
    }

    public int getState() {
        return father.GetState();
    }
}
