using UnityEngine;
using Unity.Cinemachine;

public class DialogueCameraController : MonoBehaviour
{
    public static DialogueCameraController Instance;

    [Header("Referências")]
    public CinemachineFreeLook dialogueCamera;
    public Transform player;

    private Transform currentNPC;

    private void Awake()
    {
        Instance = this;
        dialogueCamera.gameObject.SetActive(false);
    }

    public void ActivateDialogueCamera(Transform npc)
    {
        currentNPC = npc;

        Vector3 midPoint = (player.position + npc.position) / 2f;

        GameObject focusPoint = new GameObject("DialogueFocusPoint");
        focusPoint.transform.position = midPoint;

        dialogueCamera.Follow = focusPoint.transform;
        dialogueCamera.LookAt = npc;

        dialogueCamera.gameObject.SetActive(true);
    }

    public void DeactivateDialogueCamera()
    {
        dialogueCamera.gameObject.SetActive(false);
        dialogueCamera.Follow = null;
        dialogueCamera.LookAt = null;

        if (currentNPC != null)
            Destroy(GameObject.Find("DialogueFocusPoint"));

        currentNPC = null;
    }
}