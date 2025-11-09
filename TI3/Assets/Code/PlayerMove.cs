using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    PlayerAction playerActions;
    public InputAction move;
    Vector3 moveInput;
    [SerializeField] CharacterController controller;
    [SerializeField] public float speed = 5f;
    bool isChildColliding = false;

    void Awake()
    {
        playerActions = new PlayerAction();
        move = playerActions.Player.Move;
    }
    
    void Start()
    {
        move.performed += ctx => moveInput = transform.forward * ctx.ReadValue<Vector2>().y + transform.right * ctx.ReadValue<Vector2>().x;
        move.canceled += ctx => moveInput = Vector3.zero;
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
     public void SetCollisionStatus(bool colliding)
    {
        isChildColliding = colliding;
    }
}
