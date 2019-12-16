using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    public enum TrapState
    {
        Active,
        Reactivate,
        Triggered,
        Deactive
    }

    protected Collider detectionArea;
    protected Animator animator;

    protected GameObject[] players;

    protected float triggerChance;
    public TrapState currentState;
    protected float reactivateTime;
    float currentTime = 0;

    public GameObject resetSign;
    protected GameObject reset;

    protected void Start()
    {
        currentState = TrapState.Active;
        reset = Instantiate(resetSign, gameObject.transform);
        reset.transform.position += new Vector3(0, 2, 0);
        reset.SetActive(false);
    }

    void Update()
    {
        players = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().players;
        switch (currentState)
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
                    reset.SetActive(true);
                    currentState = TrapState.Deactive;
                }
                break;

            case TrapState.Deactive:
                if(Reset())
                {
                    reset.SetActive(false);
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
