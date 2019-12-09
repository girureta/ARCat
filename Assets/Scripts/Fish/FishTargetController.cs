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
        Utils.SetParentAndModifyScale(particles.transform, transform.parent);
        particles.transform.position = particleSpawnPoint.position;
    }
}
