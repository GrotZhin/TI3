using UnityEngine;

public class CheckRockPuzzle : MonoBehaviour
{
    private Transform player;
    public float interactRadius = 2f;
    public KeyCode interactKey = KeyCode.E;
    SimonSays simonSays;
     public GameObject bixin;
    public Animator Ani;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Ani = bixin.GetComponent<Animator>();
        simonSays = GameObject.FindGameObjectWithTag("Player").GetComponent<SimonSays>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < interactRadius)
        {
            if (Input.GetKeyDown(interactKey))
            {
                if (!simonSays.play)
                {
                     Ani.SetTrigger("Button");
                    simonSays.play = true;
                    simonSays.Play();
                    return;
                }
                if (simonSays.canPlay) {
                    Ani.SetTrigger("Button");
                    simonSays.RockObject(this.gameObject);
                    
                }
                
            }
        }
    }
}
