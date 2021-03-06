﻿using UnityEngine;
using UnityEngine.Events;

public class MapRaycastController : MonoBehaviour
{
    protected Camera camera;

    /// <summary>
    /// The layer where the objects that we want to raycast too are placed in. In this game we use a quad above the ground.
    /// </summary>
    public string layerName = "MapRaycast";
    protected int layer;

    /// <summary>
    /// Indicates that a ray was casted and it hit the map
    /// </summary>
    public RayCastHitEvent onRayCastHit = new RayCastHitEvent();

    private void Awake()
    {
        camera = MainApp.instance.camera;
        layer = 1 << LayerMask.NameToLayer(layerName);
    }

    // Update is called once per frame
    void Update()
    {
        if (InputHelper.IsTapping())
        {
            Vector3 tapPosition = InputHelper.TapPosition();
            CheckRaycast(tapPosition);
        }
    }

    protected void CheckRaycast(Vector3 screenPos)
    {
        Ray ray = camera.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
        {
            onRayCastHit.Invoke(hit.point);
            Debug.DrawLine(camera.transform.position, hit.point, Color.green, 1.0f, false);
        }

    }

    [System.Serializable]
    public class RayCastHitEvent : UnityEvent<Vector3> { }
}
