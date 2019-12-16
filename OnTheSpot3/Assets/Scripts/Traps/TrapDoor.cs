using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : Trap
{
    float openTime = 0;
    float timeToBeLeftOpen;

    private new void Start()
    {
        base.Start();
        triggerChance = 0.25f;
        detectionArea = GetComponent<BoxCollider>();
        reactivateTime = 0.5f;
        animator = GetComponent<Animator>();
        timeToBeLeftOpen = 2;
    }

    protected override bool Activate()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if(players[i].GetComponent<PlayerMovement>().isDead)
            {
                break;
            }
            if (detectionArea.bounds.Intersects(players[i].GetComponent<Collider>().bounds))
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
        }
        return false;
    }

    protected override bool Reset()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<PlayerMovement>().isDead && detectionArea.bounds.Intersects(players[i].GetComponent<Collider>().bounds))
            {
                return players[i].GetComponent<PlayerMovement>().Reactivate();
            }
        }
        return false;
    }

    protected override bool Trigger()
    {
        openTime += Time.deltaTime;
        if(openTime >= timeToBeLeftOpen)
        {
            animator.SetBool("Trigger", false);
            return true;
        }

        return false;
    }
}
