using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
public class MainMenu : MonoBehaviour
{
    [Header("UI Overlays")]
    public Transform exitOverlay;
    public Transform loadGameOverlay;
    public Transform saveSlotOverlay;
    public Transform settingsOverlay;

    // private int selectedSaveSlot = 0;
    public void NewGame()
    {
        Debug.Log("New Game clicked");
        SceneManager.LoadSceneAsync("ClassroomScene");

        PlayerSaveFile player = new PlayerSaveFile();

        player.saveFileName = "Player"; // Will change in the future to accept user request
        player.lastSaved = DateTime.Now.ToString();
        player.currentLevel = 1; // 0 = Tutorial Level
        // player.levelStates.Add();  // Add entry for tutorial level

        PlayerSaver.CreateSaveFile(player);
        PlayerSaveFile.currentSaveFile = player; // Sets newly created savefile as yung sa player
    }

    public void Continue()
    {
        Debug.Log("Continue clicked");
        loadGameOverlay.gameObject.SetActive(true);
        exitOverlay.gameObject.SetActive(true);
    }

    public void Exit()
    {
        Debug.Log("Exit clicked");
        Application.Quit();
    }

    public void CloseOverlay()
    {
        loadGameOverlay.gameObject.SetActive(false);
        saveSlotOverlay.gameObject.SetActive(false);
        exitOverlay.gameObject.SetActive(false);
    }
}
