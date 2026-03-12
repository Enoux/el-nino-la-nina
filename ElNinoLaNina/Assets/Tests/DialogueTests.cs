using NUnit.Framework;
using UnityEngine;
using TMPro;

public class DialogueTests
{
    DialogueManager manager;
    DialogueData data;

    GameObject dialogueBox;
    CanvasGroup inventory;
    TMP_Text text;

    [SetUp]
    public void Setup()
    {
        var go = new GameObject();
        manager = go.AddComponent<DialogueManager>();

        dialogueBox = new GameObject();
        text = new GameObject().AddComponent<TextMeshProUGUI>();
        inventory = new GameObject().AddComponent<CanvasGroup>();

        manager.dialogueBox = dialogueBox;
        manager.dialogueText = text;
        manager.inventoryUI = inventory;

        data = ScriptableObject.CreateInstance<DialogueData>();
        data.lines = new string[]
        {
            "Hello",
            "Second line",
            "Third line"
        };
    }

    [Test]
    public void StartDialogue_BeginsDialogue()
    {
        manager.StartDialogue(data);

        Assert.IsTrue(manager.IsPlaying);
        Assert.IsTrue(dialogueBox.activeSelf);
        Assert.AreEqual("Hello", text.text);
    }

    [Test]
    public void StartDialogue_HidesInventory()
    {
        manager.StartDialogue(data);

        Assert.AreEqual(0, inventory.alpha);
        Assert.IsFalse(inventory.interactable);
        Assert.IsFalse(inventory.blocksRaycasts);
    }

    [Test]
    public void NextLine_AdvancesDialogue()
    {
        manager.StartDialogue(data);

        manager.NextLine();

        Assert.AreEqual(1, manager.currentLine);
        Assert.AreEqual("Second line", text.text);
    }

    [Test]
    public void Dialogue_ReachesLastLine()
    {
        manager.StartDialogue(data);

        manager.NextLine();
        manager.NextLine();

        Assert.AreEqual("Third line", text.text);
    }

    [Test]
    public void Dialogue_EndRestoresInventory()
    {
        manager.StartDialogue(data);

        manager.NextLine();
        manager.NextLine();
        manager.NextLine(); // triggers EndDialogue

        Assert.IsFalse(manager.IsPlaying);
        Assert.IsFalse(dialogueBox.activeSelf);

        Assert.AreEqual(1, inventory.alpha);
        Assert.IsTrue(inventory.interactable);
        Assert.IsTrue(inventory.blocksRaycasts);
    }

    [Test]
    public void NextLine_DoesNothingIfNotPlaying()
    {
        manager.NextLine();

        Assert.AreEqual(0, manager.currentLine);
    }
}