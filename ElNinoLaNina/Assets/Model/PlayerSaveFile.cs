using System;
using System.Collections.Generic;

[Serializable]
public class PlayerSaveFile
{

    public static PlayerSaveFile currentSaveFile;
    public static bool universalDevMode = false;

    public int slot;
    public string saveFileName;
    public string lastSaved;

    public int currentLevel;
    public bool devModeEnabled;
    public bool godModeEnabled;

    public List<string> hotspotStateKeys = new();
    public List<int> hotspotStateVals = new();

    public List<ItemData> playerItems = new();
}
