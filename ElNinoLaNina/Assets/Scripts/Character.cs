using UnityEngine;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
    [Header("Character Details")]
    [SerializeField] private string characterName;

    [Header("Animation")]
    [SerializeField] public Animator characterAnim;

    [Header("Dialogue")]
    [Tooltip("Character dialogue(s) for different scenarios.")]
    public List<DialogueData> characterDialogues;
    [SerializeField] private DialogueManager dialogueManager;

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
            CharacterTalk("Dead");
        }

        // Holding nothing; trigger dialogue instead
        if (item == null)
        {
            CharacterTalk("Talk");
        }

        // Character already holding item
        if (heldItem.Count > 0) {
            CharacterTalk("Inventory Full");
            return false;
        }

        // Successfully give item to character
        heldItem.Add(item);
        OnGiveItem(item);
        return true;
    }

    protected virtual void OnGiveItem(ItemData item)
    {
        
    }

    protected virtual void CharacterTalk(string scenario)
    {
        
    }
}