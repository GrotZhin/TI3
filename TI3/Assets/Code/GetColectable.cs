using System.Collections.Specialized;
using UnityEngine;

public class GetColectable : MonoBehaviour
{
    SerializeField rockindex;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InspectUiManager manage = FindObjectOfType<InspectUiManager>();
            manage.OpenMenu();
        }
        Object.Destroy(gameObject);
    }
}
