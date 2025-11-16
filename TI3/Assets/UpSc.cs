using UnityEngine;


public class UpSc : MonoBehaviour
{
    public Animator Ani;
    public GameObject Door;
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
            Ani.SetBool("Up", true);
        }
        else
        {
            Ani.SetBool("Up", false);
        }
    }
}
