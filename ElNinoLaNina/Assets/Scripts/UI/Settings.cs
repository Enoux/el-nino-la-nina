using UnityEngine;
using TMPro;

public class Settings : MonoBehaviour
{

    public TMP_Text devModeText;

    public void ToggleDevMode() 
    {
        PlayerSaveFile.universalDevMode = !PlayerSaveFile.universalDevMode;
        Debug.Log("Universal Dev Mode: " + PlayerSaveFile.universalDevMode);
        UpdateDevModeText();
    }

    public void UpdateDevModeText() 
    {
        bool devModeEnabled = PlayerSaveFile.universalDevMode;
        devModeText.text = devModeEnabled? "ON": "OFF";
    }
}
