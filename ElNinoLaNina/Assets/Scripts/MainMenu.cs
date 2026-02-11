using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class MainMenu : MonoBehaviour
{

    public Transform loadGameOverlay;
    // public Transform saveSlotOverlay;

    // private int selectedSaveSlot = 0;
    public void NewGame()
    {
        Debug.Log("New Game clicked");
        // SceneManager.LoadSceneAsync("TestScene");

        PlayerSaveFile player = new PlayerSaveFile();
        player.saveFileName = "Yo";
        player.lastSaved = DateTime.Now.ToString();
        PlayerSaver.CreateSaveFile(player);
    }

    public void Continue()
    {
        Debug.Log("Continue clicked");
        loadGameOverlay.gameObject.SetActive(true);
    }

    public void Exit()
    {
        Debug.Log("Exit clicked");
        Application.Quit();
    }
}
