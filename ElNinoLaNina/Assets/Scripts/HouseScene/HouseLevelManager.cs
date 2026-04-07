using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.Collections;

public class HouseLevelManager : MonoBehaviour
{
    [Header("References")]
    public ItemData flashlight;
    public MotherHS mother;

    public HealthSystem healthSystem;
    public CameraController cameraController;
    public List<Viewpoint> fireViewpoints;

    // AttemptExit()
    // Should check if inventory has flashlight and door is clicked
    public void AttemptExitWindow()
    {
        List<ItemData> inventory = InventoryManager.Instance.GetItems();

        if (inventory.Contains(flashlight))
        {
            if (mother.getState() == 0) {
                Debug.Log("You cannot leave mother behind.");
            }
            else {
                Debug.Log("Window exit successful. Level complete.");
                PlayerWin();
            }
        }
        else
        {
            Debug.Log("You need to find a flashlight.");
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
        StartCoroutine(InflictSmokeDamage());
    }

    IEnumerator InflictSmokeDamage() {
        while (true) {
            for (int i = 0; i < 4; i++) {
                // Double the smoke damage if close to fire
                if (fireViewpoints.Contains(cameraController.currentView)) {
                    yield return new WaitForSeconds(0.125f);
                } else {
                    yield return new WaitForSeconds(0.25f);
                } 
            }
            
            healthSystem.TakeDamage(1, "Smoke Inhalation");
        }
    }
}