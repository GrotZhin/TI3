using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TerrainTools;
using UnityEngine.UIElements;

public class Push : MonoBehaviour
{
    [SerializeField] float power;
    private Transform player;
    [SerializeField] GameObject pai;
    public float interactRadius = 2f;
    public KeyCode interactKey = KeyCode.E;
    bool isPushing = false;
    [SerializeField] BoxPuzzle boxPuzzle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
                if (!isPushing)
                {
                    isPushing = true;
                  boxPuzzle.Pickup(this.gameObject);
                }
                else
                {   
                 isPushing = false;
                 boxPuzzle.Drop();
                }
            }
        }

       



    }
    void OnCollisionEnter(Collision collision)
    {
     
        
    }
   
}
