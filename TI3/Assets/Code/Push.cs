using Unity.VisualScripting;
using UnityEngine;

public class Push : MonoBehaviour
{
    [SerializeField] float power;
 
    public int i = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Push"))
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

          
              
            
            if (rb != null)
            {
               
                Vector3 direction = hit.gameObject.transform.position - this.transform.position;
                direction.y = 0;
                direction.Normalize();
                rb.AddForceAtPosition(direction * power, transform.position, ForceMode.Impulse);
            }
        }
    }
}
