using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomConnector : MonoBehaviour
{

    bool active;

    roomManager GameController;
    List<GameObject> players;
    int alivePlayers;
    List<GameObject> donePlayers;
    List<GameObject> playersInZone;

    GameObject rightEdge;
    BoxCollider rightWall;
    GameObject leftEdge;
    BoxCollider leftWall;

    // Start is called before the first frame update
    void Start()
    {
        active = true;
        GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<roomManager>();
        players = new List<GameObject>();
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            if (allPlayers[i].GetComponent<PlayerMovement>().isDummy == false)
            {
                players.Add(allPlayers[i]);
            }
        }
        //Debug.Log("Players: " + players.Length);
        donePlayers = new List<GameObject>();
        playersInZone = new List<GameObject>();
        alivePlayers = players.Count;

        rightEdge = transform.Find("RightEdge").gameObject;
        rightWall = rightEdge.transform.GetChild(0).GetComponentInChildren<BoxCollider>();
        leftEdge = transform.Find("LeftEdge").gameObject;
        leftWall = leftEdge.transform.GetChild(0).GetComponent<BoxCollider>();

        openLeft();
        sealRight();

        InvokeRepeating("checkAlivePlayers", 2.0f, 2f);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    void checkAlivePlayers()
    {
        if (active)
        {
            alivePlayers = players.Count;
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].transform.position.y < 0)
                {
                    alivePlayers--;
                    players[i].GetComponent<PlayerMovement>().Kill();
                } else if(players[i].GetComponent<PlayerMovement>().isDead)
                {
                    alivePlayers--;
                }
            }

            if (alivePlayers == 0)
            {
                GameObject.Find("Smoke").GetComponent<PlayerKill>().isMoving = false;
                Invoke("roomFinished",2f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            if (!other.GetComponent<PlayerMovement>().isDead)
            {
                bool alreadyFinished = false;
                foreach (var player in donePlayers)
                {
                    if (other.gameObject == player)
                    {
                        alreadyFinished = true;
                    }
                }

                if (alreadyFinished == false)
                {
                    donePlayers.Add(other.gameObject);
                    if (donePlayers.Count == 1)
                    {
                        GameController.playerFinished(other.gameObject);
                        leftEdge.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                    }
                }

                playersInZone.Add(other.gameObject);

                checkAlivePlayers();

                if (playersInZone.Count == alivePlayers)
                {
                    GameObject.Find("Smoke").GetComponent<PlayerKill>().isMoving = false;
                    roomFinished();
                }
            }    
        }

        if (!active)
        {
            playersInZone.Add(other.gameObject);
        }
        

    }

    private void OnTriggerExit(Collider other)
    {
        playersInZone.Remove(other.gameObject);

        /*
        if(active == false && playersInZone.Count == 0)
        {
            sealRight();
        }
        */
    }

    void roomFinished()
    {
        
        active = false;
        sealLeft();
        openRight();

        for (int i = 0; i < donePlayers.Count; i++)
        {
            GameController.playerFinished(donePlayers[i]);
        }

        GameController.changeRoom();
        playersInZone.Clear();
        //for (int i = 0; i < players.Length; i++)
        //	{
        //        playersInZone.Add(players[i]);   
        //	}
    }

    void openRight()
    {
        rightEdge.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        rightWall.isTrigger = true;
    }

    void openLeft()
    {
        leftEdge.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        leftWall.isTrigger = true;
    }

    void sealRight()
    {
        rightEdge.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        rightWall.isTrigger = false;
    }

    void sealLeft()
    {
        leftEdge.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        leftWall.isTrigger = false;
    }

    public void Reset()
    {
        GameObject.Find("Smoke").GetComponent<PlayerKill>().isMoving = false;
        roomFinished();
    }
}
