using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class DevConsole : MonoBehaviour
{
    [Header("UI")]
    public GameObject consoleRoot;
    public TMP_InputField inputField;
    public TMP_Text outputText;

    private Dictionary<string, Action<string[]>> commands;

    public bool IsOpen { get; private set; }

    void Awake()
    {
        commands = new Dictionary<string, Action<string[]>>()
        {
            { "help", Help },
            { "clear", Clear },
        };

        CloseConsole();
    }

    void Update()
    {
        // Toggle console with tilde (~)
        if (Keyboard.current.backquoteKey.wasPressedThisFrame)
        {
            if (IsOpen) CloseConsole();
            else OpenConsole();
        }

        if (!IsOpen) return;

        // Submit command
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            SubmitCommand(inputField.text);
            inputField.text = "";
            inputField.ActivateInputField();
        }
    }

    void OpenConsole()
    {
        consoleRoot.SetActive(true);
        IsOpen = true;

        inputField.ActivateInputField();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void CloseConsole()
    {
        consoleRoot.SetActive(false);
        IsOpen = false;
    }

    void SubmitCommand(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return;

        Log("> " + input);

        string[] split = input.Split(' ');
        string command = split[0].ToLower();

        if (commands.TryGetValue(command, out var action))
        {
            action(split);
        }
        else
        {
            Log("Unknown command: " + command);
        }
    }

    void Log(string message)
    {
        outputText.text += message + "\n";
    }

    // =========================
    // Commands
    // =========================

    void Help(string[] args)
    {
        Log("Available commands:");
        foreach (var cmd in commands.Keys)
        {
            Log("- " + cmd);
        }
    }

    void Clear(string[] args)
    {
        outputText.text = "";
    }

    
}
