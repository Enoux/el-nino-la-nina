using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.Collections;

public class CityLevelManager : MonoBehaviour
{
    [Header("References")]
    public ItemData boat;
    public ItemData paddle;
    public ItemData catto;

    public HealthSystem healthSystem;
    public CameraController cameraController;

    // AttemptExit()
    // Should check if inventory has flashlight and door is clicked
    public void AttemptExitTunnel()
    {
        List<ItemData> inventory = InventoryManager.Instance.GetItems();
        if (inventory.Contains(paddle)) {
            if (inventory.Contains(catto))
            {
                Debug.Log("Successfully evacuated. Level complete.");
                PlayerWin();
            }
            else
            {
                Debug.Log("Don't forget your cat!");
            }
        }
        else {
            Debug.Log("You need a paddle to row your boat.");
            PlayerWin();
        }
    }

    // PlayerWins()
    // Update save's current level, loads next level
    public void PlayerWin()
    {
        PlayerSaveFile.currentSaveFile.currentLevel++;
        PlayerSaveFile.currentSaveFile.lastSaved = DateTime.Now.ToString();
        PlayerSaver.UpdateSaveFile(PlayerSaveFile.currentSaveFile.slot, PlayerSaveFile.currentSaveFile);
        
        List<ItemData> itemsCopy = new(InventoryManager.Instance.GetItems());
        foreach (var item in itemsCopy) {
            InventoryManager.Instance.RemoveItem(item);
        }

        // To comment when running tests since this can only be run in Play Mode
        SceneManager.LoadSceneAsync("TestScene");
    }

    // PlayerDeath()
    // Informs death screen of cause of death, level to return to, loads death screen
    public static void PlayerDeath(string cause)
    {
        DeathData.CauseOfDeath = cause;
        SceneManager.LoadSceneAsync("DeathScreen");
    }

    void Start()
    {
        HealthData.currentHP = 100;
        HealthData.maxHP = 100;
    }
}