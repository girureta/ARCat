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
    protected Vector3 target = Vector3.zero;
    protected Vector3 currentVelocity = Vector3.zero;

    public Animator animator;
    protected int animatorIdleTrigger = Animator.StringToHash("idle");
    protected int animatorWalkTrigger = Animator.StringToHash("walk");

    public void MoveTo(Vector3 position)
    {
        target = position;
        SetWalk();
    }

    private void Update()
    {
        if (state == State.walking)
        {
            float distance = Vector3.Distance(transform.position, target);
            float timeToReach = distance / 4.0f;
            transform.position = Vector3.SmoothDamp(transform.position, target, ref currentVelocity, timeToReach);
            if (distance < 0.05)
            {
                SetIdle();
            }
        }
    }

    protected void SetWalk()
    {
        state = State.walking;
        animator.SetTrigger(animatorWalkTrigger);
    }

    protected void SetIdle()
    {
        state = State.idle;
        animator.SetTrigger(animatorIdleTrigger);
    }
}
