using UnityEngine;
using System.Collections;

public class NPCInteract : MonoBehaviour
{
    public enum PuzzleType { None, Puzzle1, Puzzle2, Puzzle3 }

    [Header("Special Behaviour")]
    public bool AllPuzzles = false;

    [Header("Dialogues")]
    public NPCDialogueData npcDataDefault;     
    public NPCDialogueData npcDataCompleted;  

    [Header("Which puzzle this NPC refers to")]
    public PuzzleType linkedPuzzle = PuzzleType.None;

    [Header("Interaction")]
    public float interactRadius = 2f;
    public KeyCode interactKey = KeyCode.E;
    private PlayerDialogueLook playerLook;
    public float lookSpeed = 5f;

    private bool dialogueCooldown = false;
    public float cooldownTime = 0.2f;
    private bool dialogueStarted = false;

    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
            playerLook = playerObj.GetComponent<PlayerDialogueLook>();
        }
    }

    void Update()
    {
        if (dialogueCooldown) return;
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);
        if (distance > interactRadius)
        {
            dialogueStarted = false;
            return;
        }

        if (dialogueStarted || (ChatBoxManager.Instance != null && ChatBoxManager.Instance.dialogueActive))
            return;

        if (Input.GetKeyDown(interactKey))
            StartDialogue();
    }

    private void StartDialogue()
    {
        if (ChatBoxManager.Instance == null) return;

        NPCDialogueData toUse = npcDataDefault;

        if (AllPuzzles)
        {
            if (GM.instance.puzzle1 && GM.instance.puzzle2 && GM.instance.puzzle3)
                toUse = npcDataCompleted;
        }
        else
        {
            switch (linkedPuzzle)
            {
                case PuzzleType.Puzzle1:
                    if (GM.instance.puzzle1) toUse = npcDataCompleted;
                    break;
                case PuzzleType.Puzzle2:
                    if (GM.instance.puzzle2) toUse = npcDataCompleted;
                    break;
                case PuzzleType.Puzzle3:
                    if (GM.instance.puzzle3) toUse = npcDataCompleted;
                    break;
            }
        }

        ChatBoxManager.Instance.StartDialogue(toUse);
        GlobalKeyBlocker.BlockKeys = true;
        DialogueCameraController.Instance.ActivateDialogueCamera(transform);

        if (playerLook != null)
            playerLook.EnableDialogueLook(transform);

        ChatBoxManager.Instance.OnDialogueEnd += ResetDialogue;
        ChatBoxManager.Instance.OnDialogueEnd += DialogueCameraController.Instance.DeactivateDialogueCamera;

        dialogueStarted = true;
    }

    private void ResetDialogue()
    {
        GlobalKeyBlocker.BlockKeys = false;

        if (playerLook != null)
            playerLook.DisableDialogueLook();

        dialogueStarted = false;
        StartCoroutine(DialogueCooldownRoutine());

        ChatBoxManager.Instance.OnDialogueEnd -= ResetDialogue;
        ChatBoxManager.Instance.OnDialogueEnd -= DialogueCameraController.Instance.DeactivateDialogueCamera;
    }

    private IEnumerator DialogueCooldownRoutine()
    {
        dialogueCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        dialogueCooldown = false;
    }
}
