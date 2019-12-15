using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowman : Trap
{
    Vector3 targetPosition;
    Vector3 currentPosition;

    public GameObject hitbox;

    float timeToMove;
    float time = 0;

    private new void Start()
    {
        base.Start();
        triggerChance = .5f;
        detectionArea = GetComponent<Collider>();
        animator = GetComponent<Animator>();
        timeToMove = .5f;
    }

    protected override bool Activate()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (detectionArea.bounds.Intersects(players[i].GetComponent<Collider>().bounds))
            {
                if (Random.Range(0, 1f) > triggerChance)
                {
                    targetPosition = players[i].transform.position;
                    RotateTowardsPosition();
                    return true;
                }
                else
                {
                    currentState = TrapState.Deactive;
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
        time += Time.deltaTime;
        if(timeToMove > time)
        {
            return false;
        }

        if(currentPosition != targetPosition)
        {
            gameObject.transform.position += (targetPosition - currentPosition).normalized;
            currentPosition = gameObject.transform.position;

            for (int i = 0; i < players.Length; i++)
            {
                if (hitbox.GetComponent<Collider>().bounds.Intersects(players[i].GetComponent<Collider>().bounds))
                {
                    players[i].GetComponent<PlayerMovement>().Kill();
                }
            }

            return false;
        }
        else
        {
            return true;
        }
    }

    void RotateTowardsPosition()
    {
        gameObject.transform.rotation = Quaternion.LookRotation(targetPosition);
    }
}
