using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Detected collisions with the cat and notifies them.
/// Destroys itself when it colliders with the cat.
/// </summary>
public class FishTargetController : MonoBehaviour
{
    public UnityEvent OnCatchedByCat = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        CatController cat = other.GetComponent<CatController>();
        if(cat != null)
        {
            OnCatchedByCat.Invoke();
            Destroy(gameObject);
        }
    }
}
