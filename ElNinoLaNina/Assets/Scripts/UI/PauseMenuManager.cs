using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;

    private bool state = false;
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame) {
            Debug.Log("Pause Pressed!");
            if (!state)
            {
                PauseGame();
            }
            else
            {
                UnpauseGame();
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        state = true;
        pauseMenu.SetActive(true);
    }

    void UnpauseGame()
    {
        Time.timeScale = 1;
        state = false;
        pauseMenu.SetActive(false);
    }

    public void ContinueGame()
    {
        UnpauseGame();
    }

    public void ExitGame()
    {
        UnpauseGame();
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void SaveGame()
    {
        PlayerSaveFile.currentSaveFile.lastSaved = DateTime.Now.ToString();
        int slot = PlayerSaveFile.currentSaveFile.slot;
        PlayerSaveFile save = PlayerSaveFile.currentSaveFile;
        PlayerSaver.UpdateSaveFile(slot, save);
    }
}
