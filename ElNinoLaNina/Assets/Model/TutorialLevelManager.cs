using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class TutorialLevelManager : MonoBehaviour, LevelManager
{
    [Header("References")]
    public ItemData flashlight;
    // AttemptExit()
    // Should check if inventory has flashlight and door is clicked
    public void AttemptWin()
    {
        List<ItemData> inventory = InventoryManager.Instance.GetItems();

        if (inventory.Contains(flashlight))
        {
            Debug.Log("Level complete");
            PlayerWin();
        }
        else
        {
            Debug.Log("You went without a flashlight. The timeline diverges.");
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
        // To comment when running tests since this can only be run in Play Mode

        SceneManager.LoadSceneAsync("HouseScene");
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