using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CollectibleItem : MonoBehaviour
{
    public ItemData item;
    private int hotspotIdx;

    void Awake()
    {
        List<string> hotspotStates = PlayerSaveFile.currentSaveFile.hotspotStateKeys;
        string name = this.gameObject.name;

        // GameObject already in hotspotStates
        if (hotspotStates.Contains(name))
        {   
            hotspotIdx = hotspotStates.IndexOf(name);

            // Item already collected; should not exist anymore
            if (PlayerSaveFile.currentSaveFile.hotspotStateVals[hotspotIdx] == 1) Destroy(this.gameObject);
        }

        else
        {
            PlayerSaveFile.currentSaveFile.hotspotStateKeys.Add(name);
            PlayerSaveFile.currentSaveFile.hotspotStateVals.Add(0);
            hotspotIdx = hotspotStates.Count - 1;
        }
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
        var playerInventory = InventoryManager.Instance;
        
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
            PlayerSaveFile.currentSaveFile.hotspotStateVals[hotspotIdx] = 1;
            Destroy(this.gameObject);
            return true;
        }
    }
}
