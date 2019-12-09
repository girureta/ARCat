using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public float speed = 4.0f;
    public float angularSpeed = 360.0f;

    public float health = 100.0f;

    internal void ApplyDamage(float damage)
    {
        health -= damage;
        Debug.LogFormat("H: {0}, Damage: {1}", health, damage);
    }

    public void MoveTo(Vector3 worldPosition)
    {
        localTarget = transform.parent.worldToLocalMatrix *  worldPosition;
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
}
