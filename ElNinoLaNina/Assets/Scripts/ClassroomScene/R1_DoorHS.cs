using UnityEngine;

public class DoorHS : HSInteract {

    [SerializeField]
    GameObject doorObject;
    private Animator doorAnim;
    private int state = 0;
    public int maxState = 2;

    void Start()
    {
        doorAnim = doorObject.GetComponent<Animator>();
    }
    protected override void OnInteract(ItemData item) {

        if (doorObject.activeSelf)
        {
            state++;
            if (state == maxState) state = 0;

            Debug.Log(state);

            doorAnim.SetInteger("state", state);
        }

        else
        {
            doorObject.SetActive(true);
        }
    }
}