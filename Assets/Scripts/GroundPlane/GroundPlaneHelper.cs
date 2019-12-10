using UnityEngine;
using Vuforia;

/// <summary>
/// Checks if a Trackable ("ground plane") is being tracked and sets isTracked accordingly.
/// </summary>
public class GroundPlaneHelper : MonoBehaviour , ITrackableEventHandler
{
    /// <summary>
    /// The Trackable that we are interested in.
    /// The ground plane.
    /// </summary>
    public TrackableBehaviour trackable;

    /// <summary>
    /// Indicates whether the Trackable is being tracked or not.
    /// </summary>
    public static bool isTracked = false;

    private void OnEnable()
    {
        trackable.RegisterTrackableEventHandler(this);
    }

    private void OnDisable()
    {
        trackable.UnregisterTrackableEventHandler(this);
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        isTracked = newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED;
    }

}
