using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TextCore.Text;

public class CharacterTests
{
    Character character;
    DialogueData dialogue;
    ItemData item1, item2;

    [SetUp]
    public void Setup()
    {
        var go = new GameObject();
        character = go.AddComponent<Character>();

        dialogue = ScriptableObject.CreateInstance<DialogueData>();
        dialogue.lines = new string[]
        {
            "Hello",
            "Second line",
            "Third line"
        };

        item1 = ScriptableObject.CreateInstance<ItemData>();
        item2 = ScriptableObject.CreateInstance<ItemData>();
    }

    [Test]
    public void CharacterTestHeldItem() {
        var result1 = character.Interact(item1);
        var result2 = character.Interact(item2);

        Assert.IsTrue(result1);
        Assert.IsFalse(result2);
    }
}
