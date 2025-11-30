using UnityEngine;

public static class GlobalKeyBlocker
{
    public static bool BlockKeys = false;

    private static readonly KeyCode[] blockedKeys =
    {
        KeyCode.Q,
        KeyCode.Escape
    };

    public static bool IsBlocked(KeyCode key)
    {
        if (!BlockKeys) return false;

        foreach (var blocked in blockedKeys)
        {
            if (key == blocked)
                return true;
        }
        return false;
    }
}
