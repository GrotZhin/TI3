using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    public NPCDialogueData npcData;
    public float interactRadius = 2f;
    public KeyCode interactKey = KeyCode.E;

    private Transform player;
    private bool isDialogueActive = false;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);
        if (distance > interactRadius) return;

        if (Input.GetKeyDown(interactKey))
        {
            if (!isDialogueActive)
            {
                ChatBoxManager.Instance.StartDialogue(npcData);
                DialogueCameraController.Instance.ActivateDialogueCamera(transform);

                isDialogueActive = true;

                ChatBoxManager.Instance.OnDialogueEnd += ResetDialogue;
                ChatBoxManager.Instance.OnDialogueEnd += DialogueCameraController.Instance.DeactivateDialogueCamera;
            }
            else
            {
                ChatBoxManager.Instance.ShowNextLine();
            }
        }
    }

    private void ResetDialogue()
    {
        isDialogueActive = false;

        ChatBoxManager.Instance.OnDialogueEnd -= ResetDialogue;
        ChatBoxManager.Instance.OnDialogueEnd -= DialogueCameraController.Instance.DeactivateDialogueCamera;
    }
}
