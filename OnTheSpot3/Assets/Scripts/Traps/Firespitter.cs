using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firespitter : Trap
{
    int amountOfShots;
    int currentShots = 0;
    float lengthOfShots;
    float time = 0;

    public List<GameObject> particles = new List<GameObject>();
    public List<GameObject> killBoxes = new List<GameObject>();
    bool fireOn = false;

    private new void Start()
    {
        base.Start();
        detectionArea = GetComponent<Collider>();
        triggerChance = .4f;
        amountOfShots = Random.Range(2, 5);
        lengthOfShots = Random.Range(1, 3);
    }

    protected override bool Activate()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<PlayerMovement>().isDead)
            {
                break;
            }
            if (detectionArea.bounds.Intersects(players[i].GetComponent<Collider>().bounds))
            {
                if (Random.Range(0, 1f) > triggerChance)
                {
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
        if(currentShots >= amountOfShots)
        {
            TurnOffFire();
            return true;
        }

        if(fireOn)
        {
            time += Time.deltaTime;
            Debug.Log(particles[0].GetComponent<ParticleSystem>().collision);
            for(int i = 0; i < players.Length; i++)
            {
                for(int j = 0; j < killBoxes.Count; j++)
                {
                    if(players[i].GetComponent<Collider>().bounds.Intersects(killBoxes[j].GetComponent<BoxCollider>().bounds))
                    {
                        players[i].GetComponent<PlayerMovement>().Kill();
                    }
                }
            }

            if(time >= lengthOfShots)
            {
                time = 0;
                TurnOffFire();
            }
        }
        else
        {
            if(currentShots == 0)
            {
                currentShots++;
                TurnOnFire();
            }
            else
            {
                time += Time.deltaTime;
                if(time >= 0.5f)
                {
                    currentShots++;
                    TurnOnFire();
                }
            }
        }
        return false;
    }

    void TurnOnFire()
    {
        for(int i = 0; i < particles.Count; i++)
        {
            particles[i].SetActive(true);
        }
        fireOn = true;
    }

    void TurnOffFire()
    {
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].SetActive(false);
        }
        fireOn = false;
    }
}
