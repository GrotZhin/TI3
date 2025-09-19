using UnityEngine;

public class GetColectable : MonoBehaviour
{
    [Header("Configurações do Coletável")]
    [SerializeField] private GameObject inspectItensCanvas;
    [SerializeField] private GameObject childToActivate;
    [SerializeField] private GameObject inspectToActivate;
    [SerializeField] private GameObject[] alwaysActive;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InspectUiManager manage = FindObjectOfType<InspectUiManager>();
            if (manage != null)
            {
                manage.OpenMenu();
            }

            if (inspectItensCanvas != null)
            {
                inspectItensCanvas.SetActive(true);

                foreach (Transform child in inspectItensCanvas.transform)
                {
                    if (!IsAlwaysActive(child.gameObject))
                    {
                        child.gameObject.SetActive(false);
                    }
                }

                if (childToActivate != null)
                {
                    childToActivate.SetActive(true);
                }

                if (inspectToActivate != null)
                {
                    inspectToActivate.SetActive(true);
                }
            }

            Destroy(gameObject);
        }
    }

    private bool IsAlwaysActive(GameObject obj)
    {
        foreach (var go in alwaysActive)
        {
            if (go == obj)
                return true;
        }
        return false;
    }
}
