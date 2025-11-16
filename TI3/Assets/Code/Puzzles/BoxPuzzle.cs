
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class BoxPuzzle : MonoBehaviour
{
    GM gm;
    [SerializeField] float power;
    [SerializeField] float pickupRange;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject holdObject;
    Rigidbody holdObjectRb;
    [SerializeField] Transform carryPoint;
    [SerializeField] Transform dropPoint;
    [SerializeField] LayerMask pickupLayer;
    Vector3 direction;
    [SerializeField] HingeJoint hinge;
    public static BoxPuzzle boxPuzzle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        winPanel.SetActive(false);
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.counterBox >= 4)
        {
            Win();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (holdObject == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, pickupRange, pickupLayer))
                {

                    if (hit.collider.CompareTag("Push"))
                    {
                        Debug.Log("colidiu");
                        Pickup(hit.collider.gameObject);
                    }
                }
            }
            else Drop();
        }

    }
    [ContextMenu("Win")]
    void Win()
    {
        Porta();
        gm.puzzle2 = true;
        winPanel.SetActive(true);

    }
    public void Pickup(GameObject hold)
    {
        if (hold.GetComponent<Rigidbody>() != null)
        {
            holdObject = hold;
            holdObjectRb = hold.GetComponent<Rigidbody>();

            holdObjectRb.isKinematic = true;
            holdObjectRb.transform.position = carryPoint.position;
            holdObject.transform.parent = carryPoint;

        }

    }
    public void Drop()
    {
        if (holdObject != null)
        {
            holdObjectRb.isKinematic = false;
            holdObject.transform.position = dropPoint.position;
            holdObject.transform.parent = null;

            holdObject = null;
            holdObjectRb = null;
        }
    }
    
    void Porta()
    {
        var motor = hinge.motor;
        motor.force = 100;
        motor.targetVelocity = 90;
        motor.freeSpin = false;
        hinge.motor = motor;
        hinge.useMotor = true;

    }
}
