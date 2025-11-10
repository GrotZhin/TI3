using UnityEngine;

public class HingeControll : MonoBehaviour
{
    [SerializeField] HingeJoint hinge;
    [SerializeField] Laser laser;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
          
          hinge = GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
       // if (laser.win == true)
        //{

        //}
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
