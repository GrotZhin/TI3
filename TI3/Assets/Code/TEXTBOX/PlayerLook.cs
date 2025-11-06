using UnityEngine;

public class PlayerDialogueLook : MonoBehaviour
{
    [Header("Referência da Mesh do Player")]
    public Transform playerMesh;
    [Header("Configuração")]
    public float lookSpeed = 5f;

    private bool isLooking = false;
    private Transform target;
    private Quaternion originalRotation;

    void Update()
    {
        if (!isLooking || target == null || playerMesh == null)
            return;

        Vector3 lookDir = target.position - playerMesh.position;
        lookDir.y = 0f;

        if (lookDir.sqrMagnitude < 0.0001f)
            return;

        Quaternion targetRot = Quaternion.LookRotation(lookDir);
        playerMesh.rotation = Quaternion.Lerp(
            playerMesh.rotation,
            targetRot,
            Time.deltaTime * lookSpeed
        );
    }

    public void EnableDialogueLook(Transform npc)
    {
        if (playerMesh != null)
            originalRotation = playerMesh.rotation;

        target = npc;
        isLooking = true;
    }

    public void DisableDialogueLook()
    {
        isLooking = false;
        target = null;

        if (playerMesh != null)
            StartCoroutine(RestoreRotation());
    }

    private System.Collections.IEnumerator RestoreRotation()
    {
        float t = 0f;
        float duration = 0.3f; 
        Quaternion startRot = playerMesh.rotation;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            playerMesh.rotation = Quaternion.Slerp(startRot, originalRotation, t);
            yield return null;
        }

        playerMesh.rotation = originalRotation; 
    }
}
