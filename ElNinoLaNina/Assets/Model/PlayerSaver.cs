#nullable enable
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

public static class SaveFiles
{
    public static DirectoryInfo directory = new DirectoryInfo("../ElNinoLaNina/Assets/SaveFiles");
    public static FileInfo[] files = directory.GetFiles("*.json");
} 
public class PlayerSaver : MonoBehaviour
{   
    public void CreateSaveFile(PlayerSaveFile playerData) {
        int saveFileSlot = 1;

        // Check which save file is available
        foreach(FileInfo file in SaveFiles.files) {
            if (!file.Name.Contains(saveFileSlot.ToString())) break;
            else saveFileSlot++;
        }

        // Dynamically determine path for new savefile
        String path = $"../ElNinoLaNina/Assets/SaveFiles/SaveFile_{saveFileSlot}.json";

        // Generate JSON string from class data
        string saveData = JsonUtility.ToJson(playerData);

        // Write JSON string into file
        File.WriteAllText(path, saveData);
    }

    public bool UpdateSaveFile(PlayerSaveFile playerData) {
        string saveFilePath = "";

        // Look for savefile to be updated based on saveFileName
        foreach(FileInfo file in SaveFiles.files) {
        PlayerSaveFile fileData = JsonUtility.FromJson<PlayerSaveFile>(File.ReadAllText(file.FullName));
            if (fileData.saveFileName == playerData.saveFileName) {
                saveFilePath = file.FullName;
                break;
            }
        }

        // No saveFile found w/ that name
        if (saveFilePath == "") return false;

        // Generate JSON string from given data
        string saveData = JsonUtility.ToJson(playerData);

        // Update JSON string in file
        File.WriteAllText(saveFilePath, saveData);
        return true;
    }

    public PlayerSaveFile? LoadSaveFile(String saveFileName)
    {
        PlayerSaveFile? playerSaveFile = null;

        // Look for save file with matching name
        foreach(FileInfo file in SaveFiles.files) {
            PlayerSaveFile fileData = JsonUtility.FromJson<PlayerSaveFile>(File.ReadAllText(file.FullName));
            if (fileData.saveFileName == saveFileName) {
                playerSaveFile = fileData;
                break;
            }
        }

        return playerSaveFile;
    }

    [ContextMenu("TestSaveFile")]
    public void Testing()
    {
        PlayerSaveFile player = new PlayerSaveFile();
        player.saveFileName = "Yo";
        player.lastSaved = DateTime.Now.ToString();
        CreateSaveFile(player);
    }
}


