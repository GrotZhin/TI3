using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    PlayerAction playerActions;
    public InputAction move;
    Vector3 moveInput;
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 5f;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float waitRotationPercentage = 20f;

    void Awake()
    {
        playerActions = new PlayerAction();
        move = playerActions.Player.Move;
    }
    
    void Start()
    {
        move.performed += ctx => moveInput = Vector3.forward * ctx.ReadValue<Vector2>().y + Vector3.right * ctx.ReadValue<Vector2>().x;
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
        Quaternion rot;
        if(moveInput != Vector3.zero) rot = Quaternion.LookRotation(moveInput);
        else rot = transform.rotation;
        if(moveInput != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * rotationSpeed);
        if ((Quaternion.Angle(transform.rotation, rot) / 180 * 100) < waitRotationPercentage) controller.Move((speed * moveInput + Vector3.up * -10) * Time.deltaTime);
    }
    
}
