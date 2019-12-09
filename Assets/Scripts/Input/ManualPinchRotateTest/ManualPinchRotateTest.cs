using UnityEngine;

public class ManualPinchRotateTest : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        if (InputHelper.IsPinching())
        {
            float delta = InputHelper.PinchDelta();
            delta = delta / Screen.width;
            float currentScale = target.lossyScale.y;
            float newScale = Mathf.Clamp(currentScale + delta,0.1f,2.0f);

            target.localScale = Vector3.one * newScale;
        }

        if (InputHelper.IsRotating())
        {
            float delta = InputHelper.RotationDelta();
            target.rotation *= Quaternion.Euler(0.0f, delta, 0.0f);
        }
    }
}
