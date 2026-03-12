using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI")]
    public GameObject dialogueBox;
    public TMP_Text dialogueText;

    [Header("Other UI")]
    public CanvasGroup inventoryUI;

    private string[] lines;
    public int currentLine { get; private set; }

    public bool IsPlaying { get; private set; }

    void Awake(){
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void StartDialogue(DialogueData data) {
        if (data == null) {
            return;
        }

        lines = data.lines;
        currentLine = 0;

        IsPlaying = true;

        dialogueBox.SetActive(true);
        inventoryUI.alpha = 0;
        inventoryUI.interactable = false;
        inventoryUI.blocksRaycasts = false;


        dialogueText.text = lines[currentLine];
    }

    public void NextLine() {
        if (!IsPlaying) {
            return;
        }

        currentLine++;

        if (currentLine >= lines.Length) {
            EndDialogue();
            return;
        }

        dialogueText.text = lines[currentLine];
    }

    void EndDialogue() {
        dialogueBox.SetActive(false);
        inventoryUI.alpha = 1;
        inventoryUI.interactable = true;
        inventoryUI.blocksRaycasts = true;

        IsPlaying = false;
    }
}