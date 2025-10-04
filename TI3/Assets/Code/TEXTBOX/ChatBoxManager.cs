using System.Collections;
using UnityEngine;
using TMPro;

public class ChatBoxManager : MonoBehaviour
{
    public static ChatBoxManager Instance;

    [Header("UI")]
    public GameObject chatBoxCanvas;
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    [Header("Configuração")]
    public float typingSpeed = 0.03f;

    [Header("Player")]
    public PlayerMove playerMove;

    private string[] lines;
    private int currentLineIndex = 0;
    private Coroutine typingCoroutine;
    private bool isTyping = false;

    public event System.Action OnDialogueEnd;

    private void Awake()
    {
        Instance = this;
        chatBoxCanvas.SetActive(false);
    }

    public void StartDialogue(NPCDialogueData npcData)
    {
        chatBoxCanvas.SetActive(true);

        if (playerMove != null)
            playerMove.enabled = false;

        nameText.text = npcData.npcName;
        nameText.color = npcData.nameColor;
        dialogueText.color = npcData.textColor;

        lines = npcData.dialogueLines;
        currentLineIndex = 0;

        ShowNextLine();
    }

    public void ShowNextLine()
    {
        if (isTyping)
        {
            CompleteLineInstantly();
            return;
        }

        if (currentLineIndex >= lines.Length)
        {
            EndDialogue();
            return;
        }

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeLine(lines[currentLineIndex]));
        currentLineIndex++;
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    private void CompleteLineInstantly()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        dialogueText.text = lines[currentLineIndex - 1];
        isTyping = false;
    }

    private void EndDialogue()
    {
        chatBoxCanvas.SetActive(false);

        if (playerMove != null)
            playerMove.enabled = true;

        OnDialogueEnd?.Invoke();
    }
}
