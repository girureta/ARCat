using UnityEngine;

/// <summary>
/// Contains basic information about a Panel.
/// Allow us enabling/disabling a panel with animations.
/// </summary>
public class PanelController : MonoBehaviour
{
    [Tooltip("The panel's animator")]
    public Animator animator;

    /// <summary>
    /// Trigger used to play the "disable" animation.
    /// </summary>
    protected int animatorDisable = Animator.StringToHash("Disable");

    /// <summary>
    /// Enables the panel, just enabling the GameObject trigger the "Enable" animation.
    /// </summary>
    public void Enable()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Disables the panel, but first it plays an animation.
    /// </summary>
    public void Disable()
    {
        animator.SetTrigger(animatorDisable);
    }

    /// <summary>
    /// Called by an Animation event to indicate that the "disable" animation ended.
    /// </summary>
    protected void OnFadedOut()
    {
        gameObject.SetActive(false);
    }
}
