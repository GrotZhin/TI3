using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Sfx;

public class ChatBoxManager : MonoBehaviour
{
    public Transform Chat;
    public static ChatBoxManager Instance;
    public TextMeshProUGUI skipMessageText;

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
    public bool dialogueActive = false;
    private bool dialogueSkipped = false;

    public event System.Action OnDialogueEnd;

    private void Awake()
    {
        Instance = this;
        chatBoxCanvas.SetActive(false);
        ShowSkipMessage(false);
    }
    private void Start()
    {
        if(AnalyticsController.Self != null) AnalyticsController.Self.StartAnly("ChatBoxManager", 0);
        if(AnalyticsController.Self != null) AnalyticsController.Self.StartAnly("SkipDialogue", 0);
    }
    private void Update()
    {
        if (!dialogueActive) {Chat.DOScaleX(0.8f,0.3f).SetEase(Ease.InOutBounce).SetUpdate(true);
        return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if(AnalyticsController.Self != null) AnalyticsController.Self.UpdateAnlyValue("SkipDialogue");
            dialogueSkipped = true;
            if (isTyping)
                CompleteLineInstantly();

            ForceEndDialogue();
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) && !dialogueSkipped)
        {
            if(AnalyticsController.Self != null) AnalyticsController.Self.UpdateAnlyValue("ChatBoxManager");
            soundManager.PlayTalk(SoundType.Talk);
            ShowNextLine();
            return;
        }
    }

    public void StartDialogue(NPCDialogueData npcData)
    {
        Chat.DOScaleX(1.5f,0.3f).SetEase(Ease.InOutBounce).SetUpdate(true);
        Chat.DOScaleX(1f,0.3f).SetUpdate(true);
        dialogueActive = true;
        dialogueSkipped = false;
        chatBoxCanvas.SetActive(true);
        ShowSkipMessage(true);
        
        soundManager.PlaySound(SoundType.Talk);

        GlobalKeyBlocker.BlockKeys = true;

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
        if (!dialogueActive || dialogueSkipped)
            return;

        if (isTyping)
        {
            CompleteLineInstantly();
            return;
        }

        if (currentLineIndex >= lines.Length)
        {
            ForceEndDialogue();
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

        if (currentLineIndex > 0 && currentLineIndex <= lines.Length)
            dialogueText.text = lines[currentLineIndex - 1];
        isTyping = false;
        
    }

    public void ForceEndDialogue()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        dialogueActive = false;

        ShowSkipMessage(false);
        chatBoxCanvas.SetActive(false);

        if (playerMove != null)
            playerMove.enabled = true;

        GlobalKeyBlocker.BlockKeys = false;

        OnDialogueEnd?.Invoke();
        
    }

    public void ShowSkipMessage(bool show)
    {
        if (skipMessageText != null)
            skipMessageText.gameObject.SetActive(show);
    }
}
