using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    public Animator animator;
    public Animator animator1;
    public GameObject pedra1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pedra1.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Push"))
        {
            pedra1.SetActive(true);
            animator.SetTrigger("plate");
            animator1.SetTrigger("abrir");
            
        }
    }
}
