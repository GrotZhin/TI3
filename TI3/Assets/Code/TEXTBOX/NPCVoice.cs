using UnityEngine;

[CreateAssetMenu(fileName = "NPCVoice", menuName = "Dialogue/NPC Voice")]
public class NPCVoice : ScriptableObject
{
    public AudioClip[] blips;

    [Header("Pitch Range")]
    public float pitchMin = 0.9f;
    public float pitchMax = 1.1f;

    [Header("Volume")]
    public float volume = 1f;
}