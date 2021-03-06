﻿using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Basic definition of a Game.
/// 
/// The expected flow is:
/// A prefab with a class that inherits from BaseGame is instantiated.
/// LoadGame.
/// StartGame.
/// PauseGame.
/// EndGameplay.
/// OnGameplayEndedis triggered.
/// QuitGame - If the user want to quit the game, or internally if the end game condition is met.
/// OnGameQuit is triggered.
/// </summary>
public abstract class BaseGame : MonoBehaviour
{
    /// <summary>
    /// Loads the required elements of the game.
    /// </summary>
    /// <returns>The operation that indicates the status of the process</returns>
    public abstract GameOperation LoadGame();

    /// <summary>
    /// Starts the actual game
    /// </summary>
    public abstract void StartGame();

    /// <summary>
    /// Pauses the game
    /// </summary>
    public abstract void PauseGame();

    /// <summary>
    /// Ends the gameplay.
    /// </summary>
    public abstract void EndGameplay();

    /// <summary>
    /// The event that indicates that the gameplay finished. The end game condition is met.
    /// It doesn't mean that the whole game is done executing
    /// </summary>
    public UnityEvent OnGameplayEnded = new UnityEvent();

    /// <summary>
    /// Quits the game, which will clean up all the elements created and will eventually trigger
    /// OnGameFinished.
    /// The is consideted to be finishes when OnGameFinished is triggered.
    /// </summary>
    /// <returns>The operation that indicates the status of the process</returns>
    public abstract GameOperation QuitGame();

    /// <summary>
    /// Indicates that the game is done, the objects were distroyed.
    /// </summary>
    public UnityEvent OnGameQuit = new UnityEvent();

    /// <summary>
    /// Indicates when the games was loaded
    /// </summary>
    public UnityEvent OnGameLoaded = new UnityEvent();

    protected State state = State.unloaded;

    public State GetState()
    {
        return state;
    }

    /// <summary>
    /// Indicates the status of an async operation in the game.
    /// </summary>
    public class GameOperation
    {
        /// <summary>
        /// Indicates whether the operation is done or not.
        /// </summary>
        public bool isDone = false;
    }

    [System.Serializable]
    public enum State
    {
        unloaded,
        loaded,
        started,
        ended,
        quit,
    }
}
