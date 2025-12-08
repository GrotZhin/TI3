using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitRoom3 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Transform player;
    public float interactRadius = 2f;
    public KeyCode interactKey = KeyCode.E;


    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
          
        }
    }

    // Update is called once per frame
    void Update()
    {
          if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);

       
      
            if (distance <= interactRadius && Input.GetKeyDown(interactKey))
            {
            SceneManager.LoadScene("Prototype");     
           
            }
    }
}
