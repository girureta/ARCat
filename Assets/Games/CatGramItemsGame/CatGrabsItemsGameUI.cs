using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatGrabsItemsGameUI : MonoBehaviour
{
    public GameObject canvas;
    public CatGrabsItemsGame game;
    public TextPanelController catchedTargets;
    public TextPanelController timer;
    public HealthPanelController healthPanel;
    public PanelController quickButtonPanel;
    public EndGamePanelController endGamePanel;

    private void OnEnable()
    {
        game.OnGameLoaded.AddListener(OnGameWasLoaded);
        game.OnGameplayEnded.AddListener(OnGameplayEnded);
    }

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
