using UnityEngine;

public class OpenGate : MonoBehaviour
{
  
    public bool open = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
        void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OpenGate"))
        {
            open = true;
            Debug.Log("o portao>: " + open);
        }
    }
     void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("OpenGate"))
        {
            open = false;
             Debug.Log("o portao>: " + open);
        }
    }
}
