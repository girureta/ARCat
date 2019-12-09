using UnityEngine;

public class MapManipulator : MonoBehaviour
{
    public Transform mapPivot;

    public float minScale = 0.1f;
    public float maxScale = 4.0f;
    public float scaleSpeed = 0.5f;

    public float rotationSpeed = 90.0f;

    // Update is called once per frame
    void Update()
    {
        if (mapPivot != null)
        {
            if (InputHelper.IsPinching())
            {
                float delta = InputHelper.PinchDelta();
                delta = (delta )  * scaleSpeed * Time.deltaTime;
                float currentScale = mapPivot.lossyScale.y;
                float newScale = Mathf.Clamp(currentScale + delta, minScale, maxScale);
                mapPivot.localScale = Vector3.one * newScale;
            }

            if (InputHelper.IsRotating())
            {
                float delta = InputHelper.RotationDelta();
                mapPivot.rotation *= Quaternion.Euler(0.0f, delta * rotationSpeed * Time.deltaTime, 0.0f);
            }
        }
    }
}
