using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class OpenCloseDoor : MonoBehaviour
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
        if(!GameUiManager.DayTime)
        {
            Ani.SetBool("Day", false);
        }
        else
        {
            Ani.SetBool("Day", true);
        }
    }
}
