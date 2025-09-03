using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    PlayerAction playerActions;
    InputAction move;
    Vector3 moveInput;
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 5f;
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
        controller.SimpleMove(moveInput * speed);
    }
}
