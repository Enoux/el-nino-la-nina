using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    public void NewGame()
    {
        Debug.Log("New Game clicked");
        SceneManager.LoadSceneAsync("TestScene");
    }

    public void Continue()
    {
        Debug.Log("Continue clicked");
        SceneManager.LoadSceneAsync("TestScene");
    }

    public void Exit()
    {
        Debug.Log("Exit clicked");
        Application.Quit();
    }
}
