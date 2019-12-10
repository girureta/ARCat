using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Detected collisions with the cat and notifies them.
/// Destroys itself when it colliders with the cat.
/// </summary>
public class FishTargetController : MonoBehaviour
{
    /// <summary>
    /// Indicates the moment the fish target is catched by the cat.
    /// </summary>
    public UnityEvent OnCatchedByCat = new UnityEvent();

    /// <summary>
    /// The location from where the particles should appear from.
    /// </summary>
    public Transform particleSpawnPoint;

    /// <summary>
    /// The prefab of the particles that are instantiated when the fish is catched.
    /// </summary>
    public ParticleSystem targedCatchedParticlesPrefab;

    public Animator animator;
    protected int animatorJumpTrigger = Animator.StringToHash("jump");

    /// <summary>
    /// The scheluded time for the fishe's next jump.
    /// </summary>
    protected float timeNextJump = 0.0f;

    private void OnEnable()
    {
        ScheludeNextJump();
    }

    private void Update()
    {
        //If the game time is larger than the scheduled time then we make the fish jump-
        if (Time.time >= timeNextJump)
        {
            animator.SetTrigger(animatorJumpTrigger);
            ScheludeNextJump();
        }
    }

    //Calculate the next time when the cat will jump
    protected void ScheludeNextJump()
    {
        timeNextJump = Time.time + GetNextJumpTime();
    }

    //Samples the distribution function for jumping.
    protected float GetNextJumpTime(float lambda = 0.5f)
    {
        return Mathf.Log(1.0f - Random.value) / (-lambda);
    }

    /// <summary>
    /// Checks if we hit the cat, meaning that we were catched.
    /// </summary>
    /// <param name="other"></param>
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

    //Spawns the particles at the given location
    protected void SpawnParticles()
    {
        var particles = GameObject.Instantiate(targedCatchedParticlesPrefab);
        Utils.SetParentAndModifyScale(particles.transform, transform.parent);
        particles.transform.position = particleSpawnPoint.position;
    }

}
