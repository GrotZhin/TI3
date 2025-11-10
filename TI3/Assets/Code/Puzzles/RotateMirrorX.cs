using UnityEngine;
using UnityEngine.InputSystem;
public class RotateMirrorX : MonoBehaviour
{
    private Transform player;
    public float interactRadius = 2f;
    [SerializeField] bool rotation = false;
    public KeyCode interactKey = KeyCode.E;
    float currentRotation = 0;
    PlayerMove playerMove;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
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
                if (!rotation)
                {
                    rotation = true;

                }
                else
                    rotation = false;

            }
            if (rotation)
            {
                playerMove.move.Disable();
                if (Input.GetKey(KeyCode.W))
                {
                    currentRotation -= 45 * Time.deltaTime;
                    transform.localEulerAngles = new Vector3(currentRotation, 0, 0);
                }


                if (Input.GetKey(KeyCode.S))
                    currentRotation += 45 * Time.deltaTime;
                transform.localEulerAngles = new Vector3(currentRotation, 0, 0);

            }
            else playerMove.move.Enable();
        }
    }
}
