using System.Collections;
using UnityEngine;


public class UpSc : MonoBehaviour
{
    public Animator Ani;
    public GameObject Blocker;
    Coroutine timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Ani = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GM.DayTime && Input.GetKeyDown(KeyCode.Q))
        {
            Blocker.SetActive(false);
            Ani.SetBool("Up", true);
            StopCoroutine(timer);
            Debug.Log("subir");
            
        }
        else if(GM.DayTime && Input.GetKeyDown(KeyCode.Q))
        {
            Ani.SetBool("Up", false);
           timer = StartCoroutine(OnAgain());
        }
    }
    IEnumerator OnAgain()
    {
        yield return new WaitForSeconds(2);
        Blocker.SetActive(true);
    }
}
