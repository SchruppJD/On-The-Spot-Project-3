using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : Trap
{
    public GameObject leftBlade;
    public GameObject rightBlade;

    Collider detectionArea;

    Animator animator;

    private new void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        currentState = TrapState.Active;
        triggerChance = .5f;
        detectionArea = GetComponent<BoxCollider>();
        reactivateTime = 0.5f;
        animator = GetComponent<Animator>();
    }

    protected override bool Activate()
    {
        if (detectionArea.bounds.Intersects(player.GetComponent<Collider>().bounds))
        {
            if (Random.Range(0, 1f) > triggerChance)
            {
                animator.SetBool("Trigger", true);
                return true;
            }
            else
            {
                currentState = TrapState.Reactivate;
                return false;
            }
        }
        return false;
    }

    protected override bool Reset()
    {
        return false;
    }

    protected override bool Trigger()
    {
        return false;
    }
}
