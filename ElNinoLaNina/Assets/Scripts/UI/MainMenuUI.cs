using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using TMPro;
public class MainMenu : MonoBehaviour
{
    [Header("UI Overlays")]
    public Transform exitOverlay;

    public Transform newGameOverlay;
    public Transform loadGameOverlay;
    public Transform saveSlotOverlay;
    public Transform settingsOverlay;

    public TMP_InputField playerName;
    public void NewGame()
    {
        Debug.Log("New Game clicked");
        newGameOverlay.gameObject.SetActive(true);
        exitOverlay.gameObject.SetActive(true);
    }

    public void Play()
    {
        SceneManager.LoadSceneAsync("ClassroomScene");

        PlayerSaveFile player = new PlayerSaveFile();

        player.saveFileName = playerName.text;
        player.lastSaved = DateTime.Now.ToString();
        player.currentLevel = 1; // 0 = Tutorial Level
        // player.levelStates.Add();  // Add entry for tutorial level

        PlayerSaver.CreateSaveFile(player);
        if (PlayerSaveFile.universalDevMode) player.devModeEnabled = true;
        PlayerSaveFile.currentSaveFile = player; // Sets newly created savefile as yung sa player
    }

    public void Continue()
    {
        Debug.Log("Continue clicked");
        loadGameOverlay.gameObject.SetActive(true);
        exitOverlay.gameObject.SetActive(true);
    }

    public void Settings()
    {
        settingsOverlay.gameObject.SetActive(true);
        exitOverlay.gameObject.SetActive(true);

        // Update Dev Mode Toggle Value
        settingsOverlay.gameObject.GetComponent<Settings>().UpdateDevModeText();
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
        settingsOverlay.gameObject.SetActive(false);
        exitOverlay.gameObject.SetActive(false);
    }
}
