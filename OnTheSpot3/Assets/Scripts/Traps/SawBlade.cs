﻿using System.Collections;
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
        //players = GameObject.FindGameObjectsWithTag("Player");
        currentState = TrapState.Active;
        triggerChance = .5f;
        detectionArea = GetComponent<BoxCollider>();
        reactivateTime = 0.5f;
        animator = GetComponent<Animator>();
    }

    protected override bool Activate()
    {
        GameObject[] players = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().players;
        for(int i = 0; i < players.Length; i++)
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
        return false;
    }

    protected override bool Trigger()
    {
        GameObject[] players = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().players;
        for(int i = 0; i < players.Length; i++)
        {
            if(players[i].GetComponent<Collider>().bounds.Intersects(leftBlade.GetComponent<Collider>().bounds))
            {
                players[i].GetComponent<PlayerMovement>().Kill();
            }
            else if (players[i].GetComponent<Collider>().bounds.Intersects(rightBlade.GetComponent<Collider>().bounds))
            {
                players[i].GetComponent<PlayerMovement>().Kill();
            }
        }
        return false;
    }
}
