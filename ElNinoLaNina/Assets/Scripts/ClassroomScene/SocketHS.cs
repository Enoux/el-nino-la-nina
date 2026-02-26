using UnityEngine;
using UnityEngine.SceneManagement;

public class SocketHS : HSInteract 
{

    [SerializeField]
    GameObject socketObject;
    public TutorialLevelManager levelManager;

    protected override void OnInteract(ItemData item) {
        // Place Code Here
        // Call level manager "I died"
        levelManager.PlayerDeath("Electrocution");
    }
}