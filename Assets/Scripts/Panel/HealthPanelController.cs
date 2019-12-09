using UnityEngine.UI;

public class HealthPanelController : PanelController
{
    public Image hearth;
    public Text text;

    public void SetHealthValue(float healthValue)
    {
        hearth.fillAmount = healthValue / 100.0f;
        text.text = healthValue.ToString("F1");
    }
}
