using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TutorialLevelManager : MonoBehaviour, LevelManager
{
    [Header("References")]
    public ItemData flashlight;
    public PlayerSaveFile save;
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
            Debug.Log("You are still missing a flashlight");
        }
    }

    // PlayerWins()
    // Update save's current level, loads next level
    public void PlayerWin()
    {
        save.currentLevel++;
        SceneManager.LoadSceneAsync("TestScene");
    }

    // PlayerDeath()
    // Informs death screen of cause of death, level to return to, loads death screen
    public void PlayerDeath(string cause)
    {
        DeathData.CauseOfDeath = cause;
        SceneManager.LoadSceneAsync("DeathScreen");
    }

}