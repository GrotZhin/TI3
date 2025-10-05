using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    GameObject activeCam;
    void Start()
    { 
        activeCam = Camera.main.gameObject;
    }

  
    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.CompareTag("Cam"))
        {
            collider.transform.GetChild(0).gameObject.SetActive(true);
            

            BoxCollider boxCollider = collider.gameObject.GetComponent<BoxCollider>();

            if (boxCollider != null) boxCollider.enabled = false;

            if (activeCam.tag != "MainCamera")
            {
               collider.transform.GetChild(0).gameObject.SetActive(false);
               activeCam.SetActive(false);
                
            }
            var currentRotation =  collider.transform.GetChild(0).gameObject.transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(0, currentRotation, 0);

            activeCam = collider.transform.GetChild(0).gameObject;
            activeCam.SetActive(true);
           
        }
        if (collider.gameObject.CompareTag("MainC") && activeCam.tag != "MainCamera")
        {

            activeCam.SetActive(false);
            activeCam = Camera.main.gameObject;
            var currentRotation = collider.gameObject.transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(0, currentRotation, 0);
        }
    }


}
