using UnityEngine;

/// <summary>
/// Simplifies getting some info from the input
/// </summary>
public static class InputHelper
{
    /// <summary>
    /// Inidicate whether the screen was tapped, or the left mouse button was released
    /// </summary>
    /// <returns></returns>
    public static bool IsTapping()
    {
        bool isTapping = false;
        isTapping = Input.touchCount == 1 || Input.GetMouseButtonUp(0);
        return isTapping;
    }

    /// <summary>
    /// Inidicates the position where the screen was tapped.
    /// </summary>
    /// <returns></returns>
    public static Vector3 TapPosition()
    {
        Vector3 position = Vector3.zero;

        if (Input.touchCount == 1)
        {
            Touch touch = Input.touches[0];
            position = touch.position;
        }

        if (Input.GetMouseButtonUp(0))
        {
            position = Input.mousePosition;
        }
        return position;
    }

}
