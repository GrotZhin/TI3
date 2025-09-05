using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    public Animator animator;
    public Animator animator1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("plate");
            animator1.SetTrigger("abrir");
            
        }
    }
}
