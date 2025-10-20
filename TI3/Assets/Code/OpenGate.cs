using UnityEngine;

public class OpenGate : MonoBehaviour
{

    public bool open = false;
    public Animator animator1;
    GM gm;

    void Start()
    {
         gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
    }
    void Update()
    {
        if ( gm.puzzle1 && gm.puzzle2 && open == true )
            animator1.SetTrigger("abrir");
    }
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
