using UnityEngine;

public class KeyBlockerLegacy : MonoBehaviour
{
    void Update()
    {
        if (!GlobalKeyBlocker.BlockKeys) return;

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape))
        {
            Input.ResetInputAxes();
        }
    }
}
