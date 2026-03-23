using UnityEngine;

public class DoorHS : HSInteract {

    [SerializeField]
    GameObject doorObject;
    public TutorialLevelManager levelManager;
    public ItemData key;
    public QTEKeyboard qte;

    void Start() {
        qte.onSuccess.AddListener(QTESuccess);
        qte.onFail.AddListener(QTEFail);
    }

    protected override void OnInteract(ItemData item = null) {
        if (base.state == 0)
        {
            if (item == key)
            {
                qte.StartQTE();
            }
            else
            {
                Debug.Log("Door is locked");
            }
            
        }
        else if (base.state == 1)
        {
            levelManager.AttemptWin();
        }
        
    }

    void QTESuccess() {
        base.state = 1;
        UpdateState();
        Debug.Log("Door unlocked");
    }

    void QTEFail() {
        Debug.Log("Unlock Failed!");
    }
}