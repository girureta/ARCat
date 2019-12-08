using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatGrabsItemsGameUI : MonoBehaviour
{
    public GameObject canvas;
    public CatGrabsItemsGame game;
    public Text catchedTargets;

    private void OnEnable()
    {
        game.OnGameLoaded.AddListener(OnGameWasLoaded);
        game.OnTargetCatched.AddListener(UpdateCatchedTargets);
        
    }

    protected void OnGameWasLoaded()
    {
        canvas.SetActive(true);
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        UpdateCatchedTargets();
    }

    protected void UpdateCatchedTargets()
    {
        catchedTargets.text = string.Format("{0}/{1}", game.catchedTargets, game.numTargets);
    }
}
