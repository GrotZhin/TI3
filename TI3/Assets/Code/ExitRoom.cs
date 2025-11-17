using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitRoom : MonoBehaviour
{

    public float pickupRange;
    public KeyCode interactKey = KeyCode.E;
    [SerializeField] LayerMask exitLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(interactKey))
        {

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, pickupRange, exitLayer))
            {

                if (hit.collider.gameObject.CompareTag("exit"))
                {
                    
                SceneManager.LoadScene("Prototype");
                }
            }


        }

    }
}