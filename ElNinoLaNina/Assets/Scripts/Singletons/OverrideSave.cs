using System;
using UnityEngine;

public class OverrideSave : MonoBehaviour
{
    public static bool overrideMode = true;
    // NOTE: Creates Save file so you can play any scene directly
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void CreateSaveFile() {
        if (!overrideMode) {
            return;
        }

        Debug.Log("OVERRIDE: Add Save File starting at ClassroomScene.");

        PlayerSaveFile player = new PlayerSaveFile
        {
            saveFileName = "OVERRIDE", // Will change in the future to accept user request
            lastSaved = DateTime.Now.ToString(),
            currentLevel = 1 // 0 = Tutorial Level
        };

    PlayerSaver.CreateSaveFile(player);
        PlayerSaveFile.currentSaveFile = player; // Sets newly created savefile as yung sa player
    }
}
