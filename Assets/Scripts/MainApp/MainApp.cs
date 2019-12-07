using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Entry point to the game.
/// Highest level of logic.
/// </summary>
public class MainApp : MonoBehaviour
{
    /// <summary>
    /// The current state of the MainApp.
    /// </summary>
    public State state = State.Started;

    /// <summary>
    /// Notifies when the State of the MainApp changed.
    /// </summary>
    public StateChangedEvent OnStateChanged = new StateChangedEvent();

    void Start()
    {
        ChangeState(State.Started);
    }

    /// <summary>
    /// The welcome screen was acknowledged
    /// </summary>
    public void AppAcknowledged()
    {
        if (state == State.Started)
        {
            ChangeState(State.WaitingForARTarget);
        }
    }

    /// <summary>
    /// Changes the State of the MainApp and triggers the corresponding callback.
    /// </summary>
    /// <param name="newState"> The new state for MainApp</param>
    protected void ChangeState(State newState)
    {
        state = newState;
        OnStateChanged.Invoke(newState);
    }

    [System.Serializable]
    public enum State
    {
        Started,
        WaitingForARTarget,
        WaitingForUser,
        RunningGame,
    }

    [System.Serializable]
    public class StateChangedEvent : UnityEvent<State> { }
}
