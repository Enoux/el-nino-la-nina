using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
            { "goto", GoToScene },
            { "godmode", SetGodMode },
        };

        CloseConsole();
    }

    void Update()
    {
        // Toggle console with tilde (~)
        if (PlayerSaveFile.currentSaveFile.devModeEnabled &&
            Keyboard.current.backquoteKey.wasPressedThisFrame)
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

    public void OpenConsole()
    {
        consoleRoot.SetActive(true);
        IsOpen = true;

        inputField.ActivateInputField();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseConsole()
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

    void GoToScene(string[] args)
    {
        if (args.Length < 2)
        {
            Log("Usage: goto <sceneName | buildIndex>");
            return;
        }

        string arg = args[1];

        // Try index
        if (int.TryParse(arg, out int index))
        {
            if (index >= 0 && index < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadSceneAsync(index);
                Log("Loading scene index: " + index);
                return;
            }
            else
            {
                Log("Invalid build index");
                return;
            }
        }

        // Try name
        if (Application.CanStreamedLevelBeLoaded(arg))
        {
            SceneManager.LoadSceneAsync(arg);
            Log("Loading scene: " + arg);
        }
        else
        {
            Log("Scene not found: " + arg);
        }
    }

    void SetGodMode(string[] args) {
        if (args.Length < 2) {
            Log("Usage: godmode <on | off>");
            return;
        }

        string arg = args[1];

        if (arg == "on") {
            PlayerSaveFile.currentSaveFile.godModeEnabled = true;
        } else if (arg == "off") {
            PlayerSaveFile.currentSaveFile.godModeEnabled = false;
        } else {
            Log("Usage: godmode <on | off>");
        }
    }
}