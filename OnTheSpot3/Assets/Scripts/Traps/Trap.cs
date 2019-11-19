using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    protected enum TrapState
    {
        Active,
        Reactivate,
        Triggered,
        Deactive
    }

    protected float triggerChance;
    protected TrapState currentState;
    //protected GameObject[] players;
    protected float reactivateTime;
    float currentTime = 0;

    protected void Start()
    {
        //players = GameObject.FindGameObjectsWithTag("Player");
        currentState = TrapState.Active;
    }

    void Update()
    {
        switch(currentState)
        {
            case TrapState.Active:
                if(Activate())
                {
                    currentState = TrapState.Triggered;
                }
                break;

            case TrapState.Reactivate:
                if(Reactivate())
                {
                    currentState = TrapState.Active;
                }
                break;

            case TrapState.Triggered:
                if(Trigger())
                {
                    currentState = TrapState.Deactive;
                }
                break;

            case TrapState.Deactive:
                if(Reset())
                {
                    currentState = TrapState.Active;
                }
                break;
        }
    }

    protected abstract bool Activate();
    protected abstract bool Trigger();
    protected abstract bool Reset();

    bool Reactivate()
    {
        currentTime += Time.deltaTime;
        if(currentTime > reactivateTime)
        {
            currentTime = 0;
            return true;
        }
        return false;
    }
}
