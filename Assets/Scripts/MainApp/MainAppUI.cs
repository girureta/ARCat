using UnityEngine;

/// <summary>
/// Handles the UI for the MainApp.
/// </summary>
public class MainAppUI : MonoBehaviour
{
    /// <summary>
    /// Reference to the MainApp.
    /// </summary>
    public MainApp mainApp;

    /// <summary>
    /// The panel used as a "welcome screen".
    /// </summary>
    public PanelController initialPanel;

    /// <summary>
    /// The panel to let the user know the should point the phone into the ground plane marker.
    /// </summary>
    public PanelController ARTargetPanel;

    /// <summary>
    /// Confirmation panel just before starting the actual gameplay.
    /// </summary>
    public PanelController startGamePanel;

    private void OnEnable()
    {
        mainApp.OnStateChanged.AddListener(OnMainAppStateChanged);
    }

    private void OnDisable()
    {
        mainApp.OnStateChanged.RemoveListener(OnMainAppStateChanged);
    }

    /// <summary>
    /// Hides/shows panels depending on the state of the MainApp.
    /// </summary>
    /// <param name="newState">The new state of the MainApp</param>
    protected void OnMainAppStateChanged(MainApp.State newState)
    {
        switch (newState)
        {
            case MainApp.State.Started:
                initialPanel.Enable();
                break;
            case MainApp.State.WaitingForARTarget:
                initialPanel.Disable();
                if (!GroundPlaneHelper.isTracked)
                {
                    ARTargetPanel.Enable();
                }
                break;
            case MainApp.State.WaitingForUser:
                ARTargetPanel.Disable();
                startGamePanel.Enable();
                break;
            case MainApp.State.LoadingGame:
                startGamePanel.Disable();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Tells the MainApp that the initial screen was acknowledged.
    /// Called when the "Touch to start" button of the initial screen is pressed.
    /// </summary>
    public void OnAppAcknowledged()
    {
        mainApp.AppAcknowledged();
    }

    /// <summary>
    /// Lets the app know the user decicided to start the game.
    /// </summary>
    public void OnUserStartGame()
    {
        mainApp.StartGame();
    }

    /// <summary>
    /// Avoids waiting for the AR target to be found.
    /// If it is alter found then the camera will move to the determined position.
    /// </summary>
    public void SkipWaitingForARTarget()
    {
        mainApp.SkipWaitingForARTarget();
    }

}
