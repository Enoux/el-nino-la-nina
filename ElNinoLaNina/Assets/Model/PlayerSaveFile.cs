using System;
using System.Collections.Generic;
using log4net.Core;

[Serializable]
public class PlayerSaveFile
{

    public static PlayerSaveFile currentSaveFile;

    public int slot;
    public string saveFileName;
    public string lastSaved;

    public int currentLevel;

    public List<int> levelStates0 = new List<int>();

    public List<ItemData> playerItems;
}
