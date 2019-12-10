using UnityEngine;
using UnityEngine.Events;

public class CatController : MonoBehaviour
{

    /// <summary>
    /// The states the cat can be in.
    /// </summary>
    protected enum State
    {
        idle,
        walking
    }

    /// <summary>
    /// Current state of the cat.
    /// </summary>
    protected State state = State.idle;

    /// <summary>
    /// Current target of the cat, in parent's local space.
    /// </summary>
    protected Vector3 localTarget = Vector3.zero;

    /// <summary>
    /// Current velocity of the cat.
    /// </summary>
    protected Vector3 currentVelocity = Vector3.zero;

    public Animator animator;

    //Trigers used to manipulate the animator.
    protected int animatorIdleTrigger = Animator.StringToHash("idle");
    protected int animatorWalkTrigger = Animator.StringToHash("walk");

    public AudioSource audioSource;

    /// <summary>
    /// AudioClip played when the cat is hurt.
    /// </summary>
    public AudioClip painAudioClip;

    /// <summary>
    /// Particles instantiated when the cat is hurt.
    /// </summary>
    public ParticleSystem fireDamageParticle;

    /// <summary>
    /// Cat's speed.
    /// </summary>
    public float speed = 4.0f;

    /// <summary>
    /// Cat's angular speed.
    /// </summary>
    public float angularSpeed = 360.0f;

    /// <summary>
    /// Cat's health.
    /// </summary>
    public float health = 100.0f;

    /// <summary>
    /// Indicates when the cat's health changed.
    /// </summary>
    public HealthChangedEvent OnHealthChanged = new HealthChangedEvent();

    /// <summary>
    /// Applies an ammount of damage to the cat and plays the corresponding effects.
    /// </summary>
    /// <param name="damage"></param>
    public void ApplyDamage(float damage)
    {
        health -= damage;

        if (!audioSource.isPlaying)
        {
            audioSource.clip = painAudioClip;
            audioSource.Play();
        }

        var particles = GameObject.Instantiate(fireDamageParticle);
        Utils.SetParentAndModifyScale(particles.transform, transform.parent);
        particles.transform.position = audioSource.transform.position;

        OnHealthChanged.Invoke(health);
    }

    /// <summary>
    /// Makes the cat move into a certain position.
    /// </summary>
    /// <param name="worldPosition"></param>
    public void MoveTo(Vector3 worldPosition)
    {
        localTarget = transform.parent.worldToLocalMatrix * worldPosition;
        SetWalk();
    }

    private void Update()
    {
        if (state == State.walking)
        {

            float distance = Vector3.Distance(transform.localPosition, localTarget);

            //Calculates an stimated travel time given the distance and speed.
            float timeToReach = distance / speed;

            //Moves the cat smoothly taking into account its current velocity and the time that is supposed to take him to travel there.
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, localTarget, ref currentVelocity, timeToReach);

            //Rotates the cat smoothly towards the target.
            Quaternion rotationToTarget = Quaternion.FromToRotation(Vector3.forward, localTarget - transform.localPosition);
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, rotationToTarget, angularSpeed * Time.deltaTime);

            //If the cat is less than 5cm from the target then we consider the goal was reached.
            if (distance < 0.05)
            {
                SetIdle();
            }
        }
    }

    /// <summary>
    /// Changes the state of the cat to walking and triggers the walking animation
    /// </summary>
    protected void SetWalk()
    {
        if (state != State.walking)
        {
            state = State.walking;
            animator.SetTrigger(animatorWalkTrigger);
        }
    }

    /// <summary>
    /// Changes the state of the cat to idle and triggers the idle animation
    /// </summary>
    protected void SetIdle()
    {
        state = State.idle;
        animator.SetTrigger(animatorIdleTrigger);
    }

    [System.Serializable]
    public class HealthChangedEvent : UnityEvent<float> { }
}
