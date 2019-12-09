﻿using UnityEngine;

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

    public static bool IsPinching()
    {
        bool retValue = false;

        retValue = IsTouching2Fingers();
        retValue = retValue || IsPinchingMouse();

        return retValue;
    }

    public static float PinchDelta()
    {
        float delta = 0.0f;

        if (IsTouching2Fingers())
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector3 touch0Current = touch0.position;
            Vector3 touch0Previous = touch0.position + touch0.deltaPosition;

            Vector3 touch1Current = touch1.position;
            Vector3 touch1Previous = touch1.position + touch1.deltaPosition;

            float distanceCurrent = Vector3.Distance(touch0Current, touch1Current);
            float distancePrevious = Vector3.Distance(touch0Previous, touch1Previous);
            delta = distancePrevious - distanceCurrent;
        }

        if (IsPinchingMouse())
        {
            delta = Input.mouseScrollDelta.y *100.0f;
        }

        return delta;
    }

    public static float RotationDelta()
    {
        float delta = 0.0f;

        if (IsTouching2Fingers())
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector3 touch0Current = touch0.position;
            Vector3 touch0Previous = touch0.position + touch0.deltaPosition;

            Vector3 touch1Current = touch1.position;
            Vector3 touch1Previous = touch1.position + touch1.deltaPosition;

            float distanceCurrent = Vector3.Distance(touch0Current, touch1Current);
            float distancePrevious = Vector3.Distance(touch0Previous, touch1Previous);
            delta = Vector3.SignedAngle(touch0Previous - touch1Previous, touch0Current - touch1Current,Vector3.forward);
        }

        if (IsRotatingMouse())
        {
            delta = -Input.mouseScrollDelta.y * 3.0f;
        }

        return delta;
    }

    public static bool IsRotating()
    {
        bool isRotating = false;

        isRotating = IsTouching2Fingers();
        isRotating = isRotating || IsRotatingMouse();

        return isRotating;
    }

    private static bool IsTouching2Fingers()
    {
        bool retValue = false;

        if (Input.touchCount >= 2)
        {
            retValue = true;
        }

        return retValue;
    }

    private static bool IsPinchingMouse()
    {
        bool retValue = false;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            retValue = true;
        }

        return retValue;
    }

    private static bool IsRotatingMouse()
    {
        bool retValue = false;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            retValue = true;
        }

        return retValue;
    }

}
