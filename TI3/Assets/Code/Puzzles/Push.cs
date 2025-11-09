using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TerrainTools;

public class Push : MonoBehaviour
{
    [SerializeField] float power;
    private Transform player;
    [SerializeField] GameObject pai;
    public float interactRadius = 2f;
    public KeyCode interactKey = KeyCode.E;
    bool isPushing = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < interactRadius)
        {

            if (Input.GetKeyDown(interactKey))
            {
                if (!isPushing) isPushing = true;
                else isPushing = false;
            }
        }

        if (isPushing) this.gameObject.transform.SetParent(pai.transform);
        else this.gameObject.transform.parent = null;



    }
    void OnCollisionEnter(Collision collision)
    {
     
        
    }
    // void OnControllerColliderHit(ControllerColliderHit hit)
    // {
    //     if (hit.collider.CompareTag("Push"))
    //     {
    //         Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

    //         if (rb != null)
    //         {

    //             Vector3 direction = hit.gameObject.transform.position - this.transform.position;
    //             direction.y = 0;
    //             direction.Normalize();
    //             rb.AddForceAtPosition(direction * power, transform.position, ForceMode.Impulse);
    //         }
    //     }
    // }
}
