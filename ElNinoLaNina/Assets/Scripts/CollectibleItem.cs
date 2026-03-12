using UnityEngine;
using UnityEngine.InputSystem;

public class CollectibleItem : MonoBehaviour
{
    public ItemData item;
    public InventoryManager playerInventory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if object was hit
            if (hit.collider.name == this.gameObject.name)
            {

                // Check if LMB was clicked
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    CollectItem();
                }
            } 
        }
    }

    public bool CollectItem()
    {
        // Inventory full
        if (playerInventory.items.Count == playerInventory.inventorySize)
        {
            return false;
        }

        // Item picked up
        else
        {   
            playerInventory.AddItem(item);
            PlayerSaveFile.currentSaveFile.playerItems = playerInventory.GetItems();
            Destroy(this.gameObject);
            return true;
        }
    }
}
