using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SaveSlotController : MonoBehaviour
{
    public Transform loadSlotOverlay;

    public Transform saveSlotOverlay;

    private List<TMP_Text> saveNames = new List<TMP_Text>();
    private List<TMP_Text> saveTimestamps = new List<TMP_Text>();

    private TMP_Text saveSlotName;
    private TMP_Text lastSaved;
    private static int selected = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get children of save slots
        for (int i = 1; i <=4; i++)
        {
            saveNames.Add(loadSlotOverlay.GetChild(i).GetChild(0).gameObject.GetComponent<TMP_Text>());
            saveTimestamps.Add(loadSlotOverlay.GetChild(i).GetChild(1).gameObject.GetComponent<TMP_Text>());
        }

        // Get children of save slot display
        saveSlotName = saveSlotOverlay.GetChild(2).GetChild(0).gameObject.GetComponent<TMP_Text>();
        lastSaved = saveSlotOverlay.GetChild(2).GetChild(1).gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // Load savefiles if overlay is active
        if (loadSlotOverlay.gameObject.activeSelf)
        {
            List<PlayerSaveFile> saveFiles = PlayerSaver.LoadSaveFiles();
            // Reset displayed save file details
            for (int i = 0; i < 4; i++)
            {
                saveNames[i].text = "Name";
                saveTimestamps[i].text = "Last Saved";
            }

            // Load save files (if any)
            int idx = 0;
            for (int i = 0; i < 4; i++)
            {
                if (idx == saveFiles.Count) break;
                PlayerSaveFile save = saveFiles[idx];
                if (save.slot == i + 1)
                {
                    saveNames[i].text = save.saveFileName;
                    saveTimestamps[i].text = save.lastSaved;
                    idx++;
                }
            }
        }

        // Loads save slot details if overlay is active
        if (saveSlotOverlay.gameObject.activeSelf)
        {   
            PlayerSaveFile selectedSaveFile = PlayerSaver.LoadSaveFile(selected);
            saveSlotName.text = selectedSaveFile.saveFileName;
            lastSaved.text = "Last Saved:  " + selectedSaveFile.lastSaved;
        }
    }

    public void SelectSaveSlot(int slot)
    {
        // Checks if selected save file still exists
        if (PlayerSaver.LoadSaveFile(slot) != null)
        {
            Debug.Log("Opened save file!");
            saveSlotOverlay.gameObject.SetActive(true);
            loadSlotOverlay.gameObject.SetActive(false);
            selected = slot;
        }
        
        else
        {
            // To replace with a visual indicator maybe?
            Debug.Log("Save file does not exist.");
        }
    }

    public void PlaySaveSlot()
    {
        PlayerSaveFile saveFile = PlayerSaver.LoadSaveFile(selected);
        if (PlayerSaveFile.universalDevMode) saveFile.devModeEnabled = true;
        PlayerSaveFile.currentSaveFile = saveFile;
        SceneManager.LoadSceneAsync(saveFile.currentLevel);
    }

    public void DeleteSaveSlot()
    {
        loadSlotOverlay.gameObject.SetActive(true);
        saveSlotOverlay.gameObject.SetActive(false);
        Debug.Log("deleted");
        PlayerSaver.DeleteSaveFile(selected);
        // Canvas.ForceUpdateCanvases();
    }
}
