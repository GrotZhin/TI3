using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/NPC Dialogue")]
public class NPCDialogueData : ScriptableObject
{
    public string npcName;
    public Color nameColor = Color.white;
    public Color textColor = Color.white;

    [TextArea(3, 10)]
    public string[] dialogueLines;

    public NPCVoice npcVoice;
}