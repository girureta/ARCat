using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Basic definition of a Game.
/// 
/// The expected flow is:
/// A prefab with a class that inherits from BaseGame is instantiated.
/// LoadGame.
/// StartGame.
/// PauseGame.
/// QuitGame - If the user want to quit the game, or internally if the end game condition is met.
/// OnGameFinished is triggered.
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
    /// Quits the game, which will clean up all the elements created and will eventually trigger
    /// OnGameFinished.
    /// The is consideted to be finishes when OnGameFinished is triggered.
    /// </summary>
    /// <returns>The operation that indicates the status of the process</returns>
    public abstract GameOperation QuitGame();

    /// <summary>
    /// The event that indicates that the game is 100% done.
    /// </summary>
    public UnityEvent OnGameFinished = new UnityEvent();

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
}
