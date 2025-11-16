using UnityEngine;

public class IdleRandomAni : MonoBehaviour
{
    public GameObject bixin;
    public Animator Ani;
    public float Cooldown;
    float Timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        if (Timer <= 0)
        {
            Timer = Cooldown;
            Ani.SetTrigger("IdleAni");
        }
        }
}
