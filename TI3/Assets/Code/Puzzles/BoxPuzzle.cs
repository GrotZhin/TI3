
using System.Reflection;
using UnityEditor.Callbacks;
using UnityEngine;

public class BoxPuzzle : MonoBehaviour
{
    GM gm;
    [SerializeField]float power;
    [SerializeField] GameObject winPanel;
    Vector3 direction;
    [SerializeField] HingeJoint hinge;
     
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
    }
    [ContextMenu("Win")]
    void Win()
    {
        Porta();
        gm.puzzle2 = true;
        winPanel.SetActive(true);

    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Push"))
        {
            Debug.Log("aeawdhabxdjass");
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

            if (rb != null)
            {

                direction = hit.gameObject.transform.position - this.transform.position;
                direction.y = 0;
                direction.Normalize();
                rb.AddForceAtPosition(direction * power, transform.position, ForceMode.Force);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Push"))
        {
            Debug.Log("sasadsa");
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

            if (rb != null) rb.linearVelocity = Vector3.zero;
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
