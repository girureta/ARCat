using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Detected collisions with the cat and notifies them.
/// Destroys itself when it colliders with the cat.
/// </summary>
public class FishTargetController : MonoBehaviour
{
    public UnityEvent OnCatchedByCat = new UnityEvent();

    public Transform particleSpawnPoint;
    public ParticleSystem targedCatchedParticlesPrefab;

    private void OnTriggerEnter(Collider other)
    {
        CatController cat = other.GetComponent<CatController>();
        if(cat != null)
        {
            SpawnParticles();
            OnCatchedByCat.Invoke();
            Destroy(gameObject);
        }
    }

    protected void SpawnParticles()
    {
        var particles = GameObject.Instantiate(targedCatchedParticlesPrefab);
        var scale = particles.transform.localScale;
        particles.transform.transform.SetParent(transform.parent);
        scale.Scale(transform.parent.lossyScale);
        particles.transform.localScale = scale;
        particles.transform.position = particleSpawnPoint.position;
    }
}
