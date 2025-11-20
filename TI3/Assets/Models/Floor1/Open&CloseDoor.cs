using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class OpenCloseDoor : MonoBehaviour
{
    public Animator Ani;
    public GameObject Door;
    public Collider Col;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Ani = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GM.DayTime)
        {
            Ani.SetBool("Day", false);
            Col.enabled = false;
        }
        else
        {
            Ani.SetBool("Day", true);
            Col.enabled = true;
        }
    }
}
