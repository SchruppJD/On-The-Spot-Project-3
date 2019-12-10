using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRoom : Trap
{
    public List<GameObject> killBoxes = new List<GameObject>();
    float timeUp = 2f;
    float time = 0;


    private new void Start()
    {
        base.Start();
        detectionArea = GetComponent<Collider>();
        animator = GetComponent<Animator>();
        triggerChance = .3f;
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
        for (int i = 0; i < players.Length; i++)
        {
            for(int j = 0; j < killBoxes.Count; j++)
                if (killBoxes[j].GetComponent<Collider>().bounds.Intersects(players[i].GetComponent<Collider>().bounds))
                {
                    players[i].GetComponent<PlayerMovement>().Kill();
                }
        }

        time += Time.deltaTime;
        if(time >= timeUp)
        {
            animator.SetBool("Trigger", false);
            return true;
        }
        return false;
    }
}
