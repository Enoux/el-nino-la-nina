using UnityEngine;

public class BoatHS : HSInteract {

    [SerializeField]
    public ItemData boat;
    public GameObject boatModel;
    public CityLevelManager cityLevelManager;
    private int boatState = 0;

    public override void OnInteract(ItemData item = null) {
        
        if (boatState == 0 && item == boat)
        {
            boatModel.SetActive(true);
            InventoryManager.Instance.RemoveItem(boat);
            boatState = 1;
            Debug.Log("Boat placed!");
        }

        else if (boatState == 1)
        {
            cityLevelManager.AttemptExitTunnel();
        }

        else if (boatState == 0)
        {
            Debug.Log("Need a boat!");
        }
    }
}