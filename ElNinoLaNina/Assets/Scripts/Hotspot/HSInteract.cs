using UnityEngine;
using System.Collections.Generic;

abstract public class HSInteract : Hotspot
{
    [Tooltip("Items that can be used on this hotspot")]
    [SerializeField]
    protected int hotspotIdx;
    protected int state {
        get {
            return PlayerSaveFile.currentSaveFile.hotspotStateVals[hotspotIdx];
        }
        set {
            PlayerSaveFile.currentSaveFile.hotspotStateVals[hotspotIdx] = value;
        }
    }

    new void Awake()
    {
        // Run hotspot Awake()
        base.Awake();
        
        List<string> hotspotStates = PlayerSaveFile.currentSaveFile.hotspotStateKeys;
        string name = this.gameObject.name;

        // GameObject already in hotspotStates
        if (hotspotStates.Contains(name))
        {   
            hotspotIdx = hotspotStates.IndexOf(name);
        }

        else
        {
            PlayerSaveFile.currentSaveFile.hotspotStateKeys.Add(name);
            PlayerSaveFile.currentSaveFile.hotspotStateVals.Add(0);
            hotspotIdx = hotspotStates.Count - 1;
        }
    }

    public virtual void OnInteract(ItemData item) {
        // Override in derived hotspot scripts
    }
}
