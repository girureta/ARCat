using System;
using UnityEngine.UI;

public class EndGamePanelController : PanelController
{
    public Text catchedTargetsText;
    public Text remainingTimeText;

    public void SetData(int catchedTargets, float time)
    {
        string timeString = time.ToString("F1");
        catchedTargetsText.text = string.Format("Fishes: {0}", catchedTargets);
        remainingTimeText.text = string.Format("Elapsed time: {0}", timeString);
    }
}
