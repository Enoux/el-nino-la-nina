using UnityEngine;
using UnityEngine.SceneManagement;

public class SocketHS : HSInteract 
{

    [SerializeField]
    GameObject socketObject;
    public HealthSystem healthSystem;

    protected override void OnInteract(ItemData item) {
        // Place Code Here
        // Call level manager "I died"

        //Refactor: Update HP subtract 100 HPs, note everytime HP is updated we need to store the reason in case player dies
        
        healthSystem.TakeDamage(100, "Electrocution");
    }
}