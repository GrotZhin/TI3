using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;

public class PlayerMove : MonoBehaviour
{
    PlayerAction playerActions;
    public GameObject bixin;
    public Animator Ani;
    public InputAction move;
    Vector3 moveInput;
    [SerializeField] CharacterController controller;
    [SerializeField] public float speed = 5f;
    bool isChildColliding = false;

    void Awake()
    {
        playerActions = new PlayerAction();
        move = playerActions.Player.Move;
        Ani = bixin.GetComponent<Animator>();
    }
    
    void Start()
    {
        move.performed += Move;
        move.canceled += StopMove;
        
    }
    void OnEnable()
    {
       
        move.Enable();
        
    }
    void OnDisable()
    {
        move.Disable();
        
    }
    void Update()
    {
        
        controller.Move((speed * moveInput + Vector3.up * -10) * Time.deltaTime);
       
    }

    void Move(InputAction.CallbackContext ctx)
    {
        moveInput = transform.forward * ctx.ReadValue<Vector2>().y + transform.right * ctx.ReadValue<Vector2>().x;
        Ani.SetBool("Walking", true);
        Vector3 dir = new Vector3(moveInput.x, 0, moveInput.y);
        Quaternion rot = Quaternion.LookRotation(moveInput);
        bixin.transform.rotation = Quaternion.Lerp(bixin.transform.rotation, rot,1);
    }
    
    void StopMove(InputAction.CallbackContext ctx)
    {
        moveInput = Vector3.zero;
        Ani.SetBool("Walking", false);
    }
    public void SetCollisionStatus(bool colliding)
    {
        isChildColliding = colliding;
    }
    void Flip()
    {
        if (moveInput.x < 0)
            transform.GetChild(0).localEulerAngles = new Vector3(0, 90, 0);
        else if (moveInput.x > 0) transform.GetChild(0).localEulerAngles = new Vector3(0, -90, 0);
        if (moveInput.z < 0)
            transform.GetChild(0).localEulerAngles = new Vector3(0, 0, 0);
        else if (moveInput.z > 0) transform.GetChild(0).localEulerAngles = new Vector3(0, 180, 0);
        
    }
}
