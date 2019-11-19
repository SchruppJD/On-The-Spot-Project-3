using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    GameObject winPlayer;
    string winName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForPlayer();
        CheckForPlayersDead();
    }

    void CheckForPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().players;
        for(int i = 0; i < players.Length; i++)
        {
            if (gameObject.GetComponent<Collider>().bounds.Intersects(players[i].GetComponent<Collider>().bounds))
            {
                SceneManager.LoadScene(0);
            }
        }
        
    }

    void CheckForPlayersDead()
    {
        GameObject[] players = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().players;

        if(players.Length < 1)
        {
            return;
        }

        int deathCount = 0;
        for(int i = 0; i < players.Length; i++)
        {
            if(players[i].GetComponent<PlayerMovement>().isDead)
            {
                deathCount++;
            }
        }
        Debug.Log(players.Length);
        if(deathCount == players.Length)
        {
            SceneManager.LoadScene(0);
        }
    }
}
