using UnityEngine;

public class CatGrabsItemsGameUI : MonoBehaviour
{
    /// <summary>
    /// The canvas GameObject The root of the UI.
    /// </summary>
    public GameObject canvas;

    /// <summary>
    /// Reference to the game that this UI belongs to.
    /// </summary>
    public CatGrabsItemsGame game;

    /// <summary>
    /// The panel that shows how many targets were caught.
    /// </summary>
    public TextPanelController catchedTargets;

    /// <summary>
    /// The panel that shows the remaining time.
    /// </summary>
    public TextPanelController timer;

    /// <summary>
    /// The panel that shows the cat's health.
    /// </summary>
    public HealthPanelController healthPanel;

    /// <summary>
    /// The quit button.
    /// </summary>
    public PanelController quickButtonPanel;

    /// <summary>
    /// The panel that is shown at the end of the gameplay and has the number of catched fishes and the elapsed time.
    /// </summary>
    public EndGamePanelController endGamePanel;

    private void OnEnable()
    {
        game.OnGameLoaded.AddListener(OnGameWasLoaded);
        game.OnGameplayEnded.AddListener(OnGameplayEnded);
    }

    /// <summary>
    /// Subscribes to some of the game events to update the catched targets and the cat's health.
    /// </summary>
    protected void OnGameWasLoaded()
    {
        canvas.SetActive(true);
        game.OnTargetCatched.AddListener(UpdateCatchedTargets);
        game.OnCatHealthChanged.AddListener(OnCatHealthChanged);
        UpdateInfo();
    }

    protected void OnCatHealthChanged(float newHealth)
    {
        healthPanel.SetHealthValue(newHealth);
    }

    /// <summary>
    /// Hides the in-game UI and shows the end game panel
    /// </summary>
    protected void OnGameplayEnded()
    {
        catchedTargets.Disable();
        timer.Disable();
        quickButtonPanel.Disable();
        healthPanel.Disable();
        endGamePanel.Enable();
        endGamePanel.SetData(game.catchedTargets, game.gameLength - GetReamingTime());
    }

    public void EndGameplay()
    {
        game.EndGameplay();
    }

    public void AcknowledgedEndGameplay()
    {
        game.QuitGame();
    }

    private void Update()
    {
        switch (game.GetState())
        {
            case BaseGame.State.unloaded:
                break;
            case BaseGame.State.loaded:
                break;
            case BaseGame.State.started:
                UpdateTimer();
                break;
            case BaseGame.State.quit:
                canvas.SetActive(false);
                break;
            default:
                break;
        }
    }

    private void UpdateInfo()
    {
        UpdateTimer();
        UpdateCatchedTargets();
    }

    protected void UpdateTimer()
    {
        timer.text.text = GetReamingTime().ToString("F1");
    }

    protected float GetReamingTime()
    {
        float time = game.remainingTime;
        time = Mathf.Max(time, 0.0f);
        return time;
    }

    protected void UpdateCatchedTargets()
    {
        catchedTargets.text.text = string.Format("{0}/{1}", game.catchedTargets, game.numTargets);
    }
}
