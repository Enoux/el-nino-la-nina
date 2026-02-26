using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text causeOfDeathText;
    public void Retry()
    {
        SceneManager.LoadSceneAsync(PlayerSaveFile.currentSaveFile.currentLevel);
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void Start()
    {
        causeOfDeathText.text = DeathData.CauseOfDeath;
    }

}