using UnityEngine;

public class GetColectable : MonoBehaviour
{
    [Header("Textos do Item")]
    [TextArea] public string rockMetalText;
    [TextArea] public string informativeHeader;
    [TextArea] public string informativeBody;

    private static bool inspectActive = false;
    private static PlayerMove cachedPlayerMovement;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (inspectActive) return;

        InspectUiManager manage = FindObjectOfType<InspectUiManager>();

        if (manage != null)
        {
            manage.SetTexts(rockMetalText, informativeHeader, informativeBody);

            manage.OpenMenu();
            inspectActive = true;

            cachedPlayerMovement = other.GetComponent<PlayerMove>();

            if (cachedPlayerMovement != null)
                cachedPlayerMovement.enabled = false;
        }

        Destroy(gameObject);
    }

    public static void ReleaseInspect()
    {
        inspectActive = false;

        if (cachedPlayerMovement != null)
            cachedPlayerMovement.enabled = true;

        cachedPlayerMovement = null;
    }
}