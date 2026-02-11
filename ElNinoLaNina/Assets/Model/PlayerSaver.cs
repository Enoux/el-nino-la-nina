#nullable enable
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

public static class SaveFiles
{
    public static DirectoryInfo directory = new DirectoryInfo("../ElNinoLaNina/Assets/SaveFiles");
} 
public static class PlayerSaver
{   
    public static bool CreateSaveFile(PlayerSaveFile playerData) {
        FileInfo[] files = SaveFiles.directory.GetFiles("*.json");
        int saveFileSlot = 1;

        // Check which save file is available
        foreach(FileInfo file in files) {
            if (!file.Name.Contains(saveFileSlot.ToString())) break;
            else saveFileSlot++;
        }

        if (saveFileSlot == 5) return false;

        // Dynamically determine path for new savefile
        String path = $"../ElNinoLaNina/Assets/SaveFiles/SaveFile_{saveFileSlot}.json";
        playerData.slot = saveFileSlot;

        // Generate JSON string from class data
        string saveData = JsonUtility.ToJson(playerData);

        // Write JSON string into file
        File.WriteAllText(path, saveData);
        return true;
    }

    public static bool UpdateSaveFile(PlayerSaveFile playerData) {
        FileInfo[] files = SaveFiles.directory.GetFiles("*.json");
        string saveFilePath = "";

        // Look for savefile to be updated based on saveFileName
        foreach(FileInfo file in files) {
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

    public static PlayerSaveFile? LoadSaveFile(int slot)
    {
        FileInfo[] files = SaveFiles.directory.GetFiles("*.json");
        PlayerSaveFile? playerSaveFile = null;

        // Look for save file with matching name
        foreach(FileInfo file in files) {
            PlayerSaveFile fileData = JsonUtility.FromJson<PlayerSaveFile>(File.ReadAllText(file.FullName));
            if (file.Name.Contains(slot.ToString())) {
                playerSaveFile = fileData;
                break;
            }
        }

        return playerSaveFile;
    }

    public static List<PlayerSaveFile> LoadSaveFiles()
    {
        FileInfo[] files = SaveFiles.directory.GetFiles("*.json");
        List<PlayerSaveFile> saveFiles = new List<PlayerSaveFile>();

        foreach(FileInfo file in files) {
            PlayerSaveFile fileData = JsonUtility.FromJson<PlayerSaveFile>(File.ReadAllText(file.FullName));
            saveFiles.Add(fileData);
        }

        return saveFiles;
    }

    public static void DeleteSaveFile(int slot)
    {   
        FileInfo[] files = SaveFiles.directory.GetFiles("*.json");
        foreach(FileInfo file in files) {
            if (file.Name.Contains(slot.ToString())) {
                File.Delete(file.FullName);
                break;
            }
        }
    }

    // [ContextMenu("TestSaveFile")]
    // public void Testing()
    // {
    //     PlayerSaveFile player = new PlayerSaveFile();
    //     player.saveFileName = "Yo";
    //     player.lastSaved = DateTime.Now.ToString();
    //     CreateSaveFile(player);
    // }
}


