using UnityEngine;
using System.Collections;

public class NPCInteract : MonoBehaviour
{
    public NPCDialogueData npcData;
    public float interactRadius = 2f;
    public KeyCode interactKey = KeyCode.E;
    private PlayerDialogueLook playerLook;
    public float lookSpeed = 5f;

    private bool dialogueCooldown = false;
    public float cooldownTime = 0.2f;
    private bool dialogueStarted = false;

    private Transform player;
    private PlayerDialogueLook playerLooks;


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
        if (ChatBoxManager.Instance != null && ChatBoxManager.Instance.dialogueActive)
            return;

        if (Input.GetKeyDown(interactKey))
        {

            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        ChatBoxManager.Instance.StartDialogue(npcData);
        GlobalKeyBlocker.BlockKeys = true;
        DialogueCameraController.Instance.ActivateDialogueCamera(transform);
        if (playerLook != null)
            playerLook.EnableDialogueLook(transform);

        ChatBoxManager.Instance.OnDialogueEnd += ResetDialogue;
        ChatBoxManager.Instance.OnDialogueEnd += DialogueCameraController.Instance.DeactivateDialogueCamera;
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
