using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : Trap
{
    public GameObject killBox;

    float upTime = 0;
    float timeToBeUp;

    private new void Start()
    {
        base.Start();
        triggerChance = 0.75f;
        detectionArea = GetComponent<BoxCollider>();
        reactivateTime = 0.5f;
        animator = GetComponent<Animator>();
        timeToBeUp = 1;
    }

    protected override bool Activate()
    {
        for (int i = 0; i < players.Length; i++)
        {
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
        upTime += Time.deltaTime;

        for(int i = 0; i < players.Length; i++)
        {
            if(killBox.GetComponent<Collider>().bounds.Intersects(players[i].GetComponent<Collider>().bounds))
            {
                players[i].GetComponent<PlayerMovement>().Kill();
            }
        }

        if(upTime >= timeToBeUp)
        {
            animator.SetBool("Trigger", false);
            return true;
        }
        else
        {
            return false;
        }
    }
}
