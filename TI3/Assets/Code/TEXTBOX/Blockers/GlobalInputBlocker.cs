using UnityEngine;
using UnityEngine.InputSystem;

public class GlobalInputBlocker : MonoBehaviour
{
    [Header("New Input System")]
    public PlayerInput playerInput;

    private InputAction qAction;
    private InputAction escAction;

    private KeyCode[] legacyKeys = { KeyCode.Q, KeyCode.Escape, KeyCode.Space };

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
        if (!GlobalKeyBlocker.BlockKeys)
        {
            RestoreActions();
            return;
        }

        BlockLegacyKeys();

        DisableNewInputActions();
    }

    void BlockLegacyKeys()
    {
        foreach (var key in legacyKeys)
        {
            if (Input.GetKeyDown(key))
                Input.ResetInputAxes();
        }
    }

    void DisableNewInputActions()
    {
        if (qAction != null && qAction.enabled)
            qAction.Disable();

        if (escAction != null && escAction.enabled)
            escAction.Disable();
    }


    void RestoreActions()
    {
        if (qAction != null && !qAction.enabled)
            qAction.Enable();

        if (escAction != null && !escAction.enabled)
            escAction.Enable();
    }

    private InputAction FindActionByKey(Key key)
    {
        foreach (var map in playerInput.actions.actionMaps)
        {
            foreach (var action in map.actions)
            {
                foreach (var binding in action.bindings)
                {
                    if (binding.isComposite || binding.isPartOfComposite)
                        continue;

                    if (binding.path.Contains(key.ToString()))
                        return action;
                }
            }
        }
        return null;
    }
}
