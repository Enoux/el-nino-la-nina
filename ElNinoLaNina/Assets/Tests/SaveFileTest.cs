using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SaveFileTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void SaveFileUnitTesting()
    {

        // Use the Assert class to test conditions
        PlayerSaveFile player = new PlayerSaveFile();
        player.saveFileName = "Benj";
        player.lastSaved = DateTime.Now.ToString();

        PlayerSaver.CreateSaveFile(player);
        player.slot = 1;

        // Assert properly created SaveFiles 
        Assert.AreEqual(JsonUtility.ToJson(player), JsonUtility.ToJson(PlayerSaver.LoadSaveFile(1)));

        PlayerSaver.CreateSaveFile(player);

        // Assert mass loading of savefiles
        List<PlayerSaveFile> players = PlayerSaver.LoadSaveFiles();
        Assert.AreEqual(2, players.Count);
        player.slot = 1;
        Assert.AreEqual(JsonUtility.ToJson(player), JsonUtility.ToJson(players[0]));
        player.slot = 2;
        Assert.AreEqual(JsonUtility.ToJson(player), JsonUtility.ToJson(players[1]));

        // Assert update save file
        player.saveFileName = "Jewel";
        PlayerSaver.UpdateSaveFile(2, player);
        Assert.AreEqual(JsonUtility.ToJson(player), JsonUtility.ToJson(PlayerSaver.LoadSaveFile(2)));

        // Assert delete save file
        PlayerSaver.DeleteSaveFile(1);
        players = PlayerSaver.LoadSaveFiles();
        Assert.AreEqual(1, players.Count);
        Assert.AreEqual(JsonUtility.ToJson(player), JsonUtility.ToJson(players[0]));
    }
}
