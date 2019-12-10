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

    public Animator animator;
    protected int animatorJumpTrigger = Animator.StringToHash("jump");

    protected float timeNextJump = 0.0f;

    private void OnEnable()
    {
        ScheludeNextJump();
    }

    private void Update()
    {
        if (Time.time >= timeNextJump)
        {
            animator.SetTrigger(animatorJumpTrigger);
            ScheludeNextJump();
        }
    }

    protected void ScheludeNextJump()
    {
        timeNextJump = Time.time + GetNextJumpTime();
    }

    protected float GetNextJumpTime(float lambda = 0.5f)
    {
        return Mathf.Log(1.0f - Random.value) / (-lambda);
    }

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
