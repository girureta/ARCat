using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Entry point to the game.
/// Highest level of logic.
/// </summary>
public class MainApp : MonoBehaviour
{
    public Camera camera;

    /// <summary>
    /// The prefab of the Game to execute
    /// </summary>
    public BaseGame game;

    protected BaseGame gameInstance;
    protected BaseGame.GameOperation gameLoadingOperation;

    /// <summary>
    /// The current state of the MainApp.
    /// </summary>
    public State state = State.Started;

    /// <summary>
    /// Notifies when the State of the MainApp changed.
    /// </summary>
    public StateChangedEvent OnStateChanged = new StateChangedEvent();

    public static MainApp instance;

    private void Awake()
    {
        instance = this;
    }

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

    public void StartGame()
    {
        if (state == State.WaitingForUser)
        {
            gameInstance = GameObject.Instantiate(game);
            gameInstance.transform.position = Vector3.zero;
            gameInstance.transform.rotation = Quaternion.identity;

            gameLoadingOperation = gameInstance.LoadGame();
            ChangeState(State.LoadingGame);
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

    private void Update()
    {
        switch (state)
        {
            case State.WaitingForARTarget:
                OnWaitingForARTarget();
                break;
            case State.LoadingGame:
                OnLoadingGame();
                break;
        }
    }

    protected void OnLoadingGame()
    {
        if (gameLoadingOperation.isDone)
        {
            gameInstance.OnGameQuit.AddListener(OnGameFinished);
            gameInstance.StartGame();
            ChangeState(State.RunningGame);
        }
    }

    protected void OnGameFinished()
    {
        gameInstance.OnGameQuit.RemoveListener(OnGameFinished);
        Destroy(gameInstance.gameObject);
        ChangeState(State.WaitingForUser);
    }

    protected void OnWaitingForARTarget()
    {
        if (GroundPlaneHelper.isTracked)
        {
            ChangeState(State.WaitingForUser);
        }
    }

    [System.Serializable]
    public enum State
    {
        Started,
        WaitingForARTarget,
        WaitingForUser,
        LoadingGame,
        RunningGame,
    }

    [System.Serializable]
    public class StateChangedEvent : UnityEvent<State> { }
}
