using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CatController : MonoBehaviour
{
    protected enum State
    {
        idle,
        walking
    }

    protected State state = State.idle;
    protected Vector3 localTarget = Vector3.zero;
    protected Vector3 currentVelocity = Vector3.zero;

    public Animator animator;
    protected int animatorIdleTrigger = Animator.StringToHash("idle");
    protected int animatorWalkTrigger = Animator.StringToHash("walk");

    public AudioSource audioSource;
    public AudioClip painAudioClip;
    public ParticleSystem fireDamageParticle;

    public float speed = 4.0f;
    public float angularSpeed = 360.0f;

    public float health = 100.0f;

    public HealthChangedEvent OnHealthChanged = new HealthChangedEvent();

    internal void ApplyDamage(float damage)
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
        Debug.LogFormat("H: {0}, Damage: {1}", health, damage);
    }

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
            float timeToReach = distance / speed;
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, localTarget, ref currentVelocity, timeToReach);

            Quaternion rotationToTarget = Quaternion.FromToRotation(Vector3.forward, localTarget - transform.localPosition);
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, rotationToTarget, angularSpeed * Time.deltaTime);

            if (distance < 0.05)
            {
                SetIdle();
            }
        }
    }

    protected void SetWalk()
    {
        if (state != State.walking)
        {
            state = State.walking;
            animator.SetTrigger(animatorWalkTrigger);
        }
    }

    protected void SetIdle()
    {
        state = State.idle;
        animator.SetTrigger(animatorIdleTrigger);
    }

    [System.Serializable]
    public class HealthChangedEvent : UnityEvent<float> { }
}
