using Unity.VisualScripting;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public GameObject Cam;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(Cam.transform);
    }
}
