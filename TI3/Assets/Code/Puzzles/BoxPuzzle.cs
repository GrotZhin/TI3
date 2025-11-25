
using System.Collections;
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
    [SerializeField] GameObject bixin;
    [SerializeField] Animator Ani;
    Rigidbody holdObjectRb;
    [SerializeField] Transform carryPoint;
    [SerializeField] Transform dropPoint;
    [SerializeField] LayerMask pickupLayer;
    Vector3 direction;
    [SerializeField] HingeJoint hinge;
    public static BoxPuzzle boxPuzzle;
    [SerializeField] PlayerMove playerMove;
    [SerializeField] bool drop = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        winPanel.SetActive(false);
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        Ani = bixin.GetComponent<Animator>();
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
                if (Physics.Raycast(bixin.transform.position, bixin.transform.forward, out hit, pickupRange, pickupLayer))
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
            Ani.SetBool("HoldingBox",true);
            holdObject = hold;
            holdObjectRb = hold.GetComponent<Rigidbody>();

            holdObjectRb.isKinematic = true;
            holdObjectRb.transform.position = carryPoint.position;
            holdObject.transform.parent = carryPoint;

        }
    }
    public void Drop()
    {
        
        if (drop)
        {
            
            if (holdObject != null)
            {
                Ani.SetTrigger("BoxTrow");
                Ani.SetBool("HoldingBox",false);
                playerMove.move.Disable();
                StartCoroutine(EnableMove());
                holdObjectRb.isKinematic = false;
                holdObject.transform.position = dropPoint.position;
                holdObject.transform.parent = null;
                holdObject = null;
                holdObjectRb = null;


            }
        }
        else
        {
            if (holdObject != null)
            {
                playerMove.move.Disable();
                Ani.SetBool("HoldingBox",false);
                StartCoroutine(EnableMove());
                //holdObject.transform.position = dropPoint.position;
                holdObject.transform.parent = null;
                holdObjectRb.isKinematic= false;
                holdObjectRb.AddForce(bixin.transform.forward * power+Vector3.up*0.5f, ForceMode.Impulse);
                holdObject = null;
                holdObjectRb = null;
            }
            
        }
    }
    IEnumerator EnableMove()
    {
        yield return new WaitForSeconds(0.25f);
        playerMove.move.Enable();
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
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Drop"))
            drop = true;
        
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Drop"))
            drop = false;

    }
}
