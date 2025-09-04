using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class inspectItens : MonoBehaviour
{
    public Camera inspCam;
    
    public float spd = 100f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
   void Update()
    {
        

        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = inspCam.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.nearClipPlane));
        Vector3 direction = mouseWorldPosition - transform.position;


        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation * Quaternion.Euler(0, -180f, 0), spd * Time.deltaTime);
        }
    }
    
    

}


