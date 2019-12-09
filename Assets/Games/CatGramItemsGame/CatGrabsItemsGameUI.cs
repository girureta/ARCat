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
    public EndGamePanelController endGamePanel;

    private void OnEnable()
    {
        game.OnGameLoaded.AddListener(OnGameWasLoaded);
        game.OnTargetCatched.AddListener(UpdateCatchedTargets);
        game.OnGameplayEnded.AddListener(OnGameplayEnded);
    }

    protected void OnGameWasLoaded()
    {
        canvas.SetActive(true);
        UpdateInfo();
    }

    protected void OnGameplayEnded()
    {
        catchedTargets.Disable();
        timer.Disable();
        endGamePanel.Enable();
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
        float time = game.remainingTime;
        time = Mathf.Max(time, 0.0f);
        timer.text.text = time.ToString("F1");
    }

    protected void UpdateCatchedTargets()
    {
        catchedTargets.text.text = string.Format("{0}/{1}", game.catchedTargets, game.numTargets);
    }
}
