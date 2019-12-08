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
                ARTargetPanel.Enable();
                break;
            case MainApp.State.WaitingForUser:
                ARTargetPanel.Disable();
                startGamePanel.Enable();
                break;
            case MainApp.State.RunningGame:
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

}
