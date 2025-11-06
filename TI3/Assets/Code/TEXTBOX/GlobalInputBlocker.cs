using UnityEngine;
using UnityEngine.InputSystem;

public class GlobalInputBlocker : MonoBehaviour
{
    [Header("New Input System")]
    public PlayerInput playerInput;

    private InputAction qAction;
    private InputAction escAction;

    void Start()
    {
        if (playerInput != null)
        {
            qAction = FindActionByKey(Key.Q);
            escAction = FindActionByKey(Key.Escape);
        }
    }

    void Update()
    {
        if (!GlobalKeyBlocker.BlockKeys) return;

        Input.ResetInputAxes();

        if (qAction != null && qAction.enabled)
            qAction.Disable();

        if (escAction != null && escAction.enabled)
            escAction.Disable();
    }

    void LateUpdate()
    {
        if (GlobalKeyBlocker.BlockKeys) return;

        if (qAction != null && !qAction.enabled) qAction.Enable();
        if (escAction != null && !escAction.enabled) escAction.Enable();
    }

    private InputAction FindActionByKey(Key key)
    {
        foreach (var map in playerInput.actions.actionMaps)
        {
            foreach (var action in map.actions)
            {
                foreach (var binding in action.bindings)
                {
                    if (binding.isComposite || binding.isPartOfComposite) continue;
                    if (binding.path.Contains(key.ToString()))
                        return action;
                }
            }
        }
        return null;
    }
}
