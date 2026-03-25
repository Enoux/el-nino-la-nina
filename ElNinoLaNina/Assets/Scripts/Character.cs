using UnityEngine;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] public Animator characterAnim;
    [Tooltip("Various states of a character (animation-wise)")]
    public Dictionary<string, int> characterStates = new Dictionary<string, int>();
    protected int state = 0;

    [Header("Dialogue")]
    [Tooltip("Character dialogue(s) for different scenarios.")]
    public List<DialogueData> characterDialogues;

    private List<ItemData> heldItem = new List<ItemData>(); 
    private bool isAlive;

    void Awake()
    {
        isAlive = true;
    }

    void Update()
    {
        
    }

    public bool Interact(ItemData item)
    {
        // Character is already dead
        if (!isAlive)
        {
            Talk("Dead");
        }

        // Holding nothing; trigger dialogue instead
        if (item == null)
        {
            Talk("Talk");
            return false;
        }

        // Character already holding item
        if (heldItem.Count > 0) {
            Talk("Inventory Full");
            return false;
        }

        // Successfully give item to character
        heldItem.Add(item);
        OnGiveItem(item);
        Debug.Log("Success give");
        return true;
    }

    public int GetState() {
        return state;
    }

    protected virtual void OnGiveItem(ItemData item)
    {
        
    }

    protected virtual void Talk(string scenario)
    {
        // Use DialogueManager.Instance to access dialogue manager in the Scene
    }

    protected virtual void Animate(string state)
    {
        
    }
}