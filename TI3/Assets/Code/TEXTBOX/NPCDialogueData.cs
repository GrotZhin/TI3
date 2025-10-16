using UnityEngine;

[CreateAssetMenu(fileName = "NPCDialogue", menuName = "Dialogue/NPCDialogue")]
public class NPCDialogueData : ScriptableObject
{
    public string npcName;
    public Color nameColor = Color.white;
    public Color textColor = Color.white;

    [TextArea(3, 10)]
    public string[] dialogueLines;
}