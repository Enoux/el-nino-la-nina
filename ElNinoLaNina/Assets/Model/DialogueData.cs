using UnityEngine;

[CreateAssetMenu(menuName = "CustomObjects/DialogueData")]
public class DialogueData : ScriptableObject {
    [TextArea]
    public string[] lines;
}
