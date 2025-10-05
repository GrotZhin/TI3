using UnityEngine;

public class Camera2 : MonoBehaviour
{
    public GameObject camera;
    GameObject activeCam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        activeCam = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cam"))
        {
        BoxCollider boxCollider = other.gameObject.GetComponent<BoxCollider>();
        if (boxCollider != null) boxCollider.enabled = false;

        
            camera.SetActive(true);
            if(activeCam.tag != "MainCamera")

                activeCam = camera;

        }
    }
}
