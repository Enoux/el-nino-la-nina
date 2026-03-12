using UnityEngine;
using UnityEngine.SceneManagement;

public class SocketHS : HSInteract 
{

    [SerializeField]
    GameObject socketObject;
    public HealthSystem healthSystem;
    public QTEKeyboard qte;

    void Start() {
        qte.onSuccess.AddListener(QTESuccess);
        qte.onFail.AddListener(QTEFail);
    }

  protected override void OnInteract(ItemData item) {
        // Place Code Here
        // Call level manager "I died"

        //Refactor: Update HP subtract 100 HPs, note everytime HP is updated we need to store the reason in case player dies
        qte.StartQTE();
        // healthSystem.TakeDamage(100, "Electrocution");
    }

    void QTESuccess() {
        healthSystem.TakeDamage(20, "Electrocution");
    }

    void QTEFail() {
        healthSystem.TakeDamage(100, "Electrocution");
    }
}