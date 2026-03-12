using UnityEngine;

public class DoorHS : HSInteract {

    [SerializeField]
    GameObject doorObject;
    private int state = 0;
    public TutorialLevelManager levelManager;
    public ItemData key;
    public QTEKeyboard qte;

    void Start() {
        qte.onSuccess.AddListener(QTESuccess);
        qte.onFail.AddListener(QTEFail);
    }

    protected override void OnInteract(ItemData item = null) {
        if (state == 0)
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
        else if (state == 1)
        {
            levelManager.AttemptWin();
        }
        
    }

    void QTESuccess() {
        state = 1;
        Debug.Log("Door unlocked");
    }

    void QTEFail() {
        Debug.Log("Unlock Failed!");
    }
}