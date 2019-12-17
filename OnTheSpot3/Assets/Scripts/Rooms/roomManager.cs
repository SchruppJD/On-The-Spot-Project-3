using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class roomManager : MonoBehaviour
{
    public int maxRooms = 10;
    private int currentRoom;

    public GameObject[] players;
    public GameObject camera;

    public GameObject startRoom;
    float startRoomWidth;
    public GameObject endRoom;
    float endRoomWidth;
    public GameObject connectorRoom;
    float connectorRoomWidth;
    public GameObject[] roomPrefabs;

    int roomIndex;
    GameObject activeRoom;
    float activeRoomWidth;
    GameObject nextRoom;
    float nextRoomWidth;

    GameObject oldConnector;
    GameObject activeConnector;
    GameObject nextConnector;

    public int[] playerPoints;
    // Start is called before the first frame update
    void Start()
    {
        currentRoom = 1;
        players = GameObject.FindGameObjectsWithTag("Player");
        playerPoints = new int[players.Length];
        for (int i = 0; i < players.Length; i++)
        {
            //players[i] = Instantiate(players[i], new Vector3(0, 1, i), Quaternion.identity);
            playerPoints[i] = 0;
        }

        connectorRoomWidth = connectorRoom.GetComponent<BoxCollider>().size.x;
        endRoomWidth = endRoom.GetComponent<BoxCollider>().size.x;
        startRoomWidth = startRoom.GetComponent<BoxCollider>().size.x;

        //Create StartRoom
        GameObject start = Instantiate(startRoom, new Vector3(0, 0, 0), Quaternion.identity);
        activeConnector = start;
        roomIndex = Random.Range(0, roomPrefabs.Length);
        activeRoom = roomPrefabs[roomIndex];
        activeRoomWidth = activeRoom.GetComponent<BoxCollider>().size.x;
        activeRoom = Instantiate(activeRoom, new Vector3((startRoomWidth / 2) + (activeRoomWidth / 2), 0, 0), Quaternion.identity);

        nextConnector = Instantiate(connectorRoom, new Vector3((activeRoom.transform.position.x) + (activeRoomWidth / 2) + (connectorRoomWidth / 2), 0, 0), Quaternion.identity);

        updateCamera();
        GameObject.Find("Smoke").transform.position = new Vector3(activeConnector.transform.position.x - 15, GameObject.Find("Smoke").transform.position.y, GameObject.Find("Smoke").transform.position.z);
        GameObject.Find("Smoke").GetComponent<PlayerKill>().ChangeSize(activeRoomWidth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            print("R key was pressed");
            nextConnector.GetComponent<roomConnector>().Reset();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            print("T key was pressed");
            SceneManager.LoadScene(0);
        }
    }


    void newRoom()
    {
        currentRoom++;
        int newRoomIndex;
        do
        {
            newRoomIndex = Random.Range(0, roomPrefabs.Length - 1);
        } while (roomIndex == newRoomIndex);

        roomIndex = newRoomIndex;
        nextRoom = roomPrefabs[roomIndex];
        nextRoomWidth = nextRoom.GetComponent<BoxCollider>().size.x;
        float newPositionX = activeRoom.transform.position.x + (activeRoomWidth / 2) + connectorRoomWidth + (nextRoomWidth / 2);
        //Debug.Log(connectorRoom.GetComponent<BoxCollider>().size.x);
        nextRoom = Instantiate(nextRoom, new Vector3(newPositionX, 0, 0), Quaternion.identity);
        Destroy(activeRoom, 0.1f);

        activeRoom = nextRoom;
        newConnector();
        GameObject.Find("Smoke").transform.position = new Vector3(activeRoom.transform.position.x - nextRoomWidth, GameObject.Find("Smoke").transform.position.y, GameObject.Find("Smoke").transform.position.z);
        GameObject.Find("Smoke").GetComponent<PlayerKill>().ChangeSize(activeRoomWidth);


    }

    void newConnector()
    {
        Destroy(activeConnector, 1f);
        activeConnector = nextConnector;
        if (currentRoom >= maxRooms)
        {
            nextConnector = Instantiate(endRoom, new Vector3((activeRoom.transform.position.x) + (activeRoomWidth / 2) + (connectorRoomWidth / 2), 0, 0), Quaternion.identity);
        }
        else
        {
            nextConnector = Instantiate(connectorRoom, new Vector3((activeRoom.transform.position.x) + (activeRoomWidth / 2) + (connectorRoomWidth / 2), 0, 0), Quaternion.identity);
        }
    }

    void respawnPlayers()
    {

        for (int i = 0; i < players.Length; i++)
        {
            //Debug.Log(players.Length);
            //Debug.Log(players[i].transform.position.y);
            //if(players[i].transform.position.y < 0)
            //{
            players[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            players[i].transform.position = new Vector3(activeConnector.transform.position.x, 1.5f, (activeConnector.transform.position.z - 5) + (3 * i));
            players[i].GetComponent<PlayerMovement>().isDead = false;
            players[i].transform.rotation = Quaternion.Euler(0, 90, 0);
            players[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            Material baseMat = players[i].GetComponent<Renderer>().material;
            baseMat.color = new Color(baseMat.color.r, baseMat.color.g, baseMat.color.b, 1.0f);
            players[i].layer = 10;
            //}
        }
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        for (int i = allPlayers.Length - 1; i >= 0; i--)
        {
            if (allPlayers[i].GetComponent<PlayerMovement>().isDummy == true)
            {
                Destroy(allPlayers[i]);
            }
        }
    }

    void updateCamera()
    {
        //Debug.Log(activeRoom.transform.position.x);
        camera.transform.position = new Vector3(activeRoom.transform.position.x, camera.transform.position.y, camera.transform.position.z);
    }

    public void playerFinished(GameObject player)
    {
        for (int i = 0; i < playerPoints.Length; i++)
        {
            if (player == players[i])
            {
                playerPoints[i] += 100;
            }
        }
    }

    public void changeRoom()
    {
        Debug.Log("Current Points: ");
        for (int i = 0; i < players.Length; i++)
        {
            Debug.Log("Player" + (i + 1) + ": " + playerPoints[i]);

            // victory condition
            if (playerPoints[i] > 1000)
            {
                GameObject victoryRoom = Instantiate(roomPrefabs[roomPrefabs.Length - 1]);
                // find player with highest score
                int highestIndex = 0;
                for (int j = 0; j < playerPoints.Length; j++)
                {
                    if (playerPoints[j] > playerPoints[highestIndex])
                    {
                        highestIndex = j;
                    }
                }
                victoryRoom.transform.position = new Vector3(10000, 10000, 0);
                players[highestIndex].transform.position = new Vector3(9999.291f, 10001.8f, 1);
                for (int j = 0; j < players.Length; j++)
                {
                    if (j != highestIndex)
                    {
                        players[j].transform.position = new Vector3(players[highestIndex].transform.position.x - (i * 3), 
                            players[highestIndex].transform.position.y, players[highestIndex].transform.position.z - 4);
                    }
                }
                GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(players[highestIndex].transform.position.x, 
                    players[highestIndex].transform.position.y + 8, players[highestIndex].transform.position.z - 7) ;
                GameObject.FindGameObjectWithTag("MainCamera").transform.eulerAngles = new Vector3(45,0,0);  
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMove>().enabled = false;

                FindObjectOfType<ScoringManager>().VictoryText(highestIndex);
                return;
            }
        }
        newRoom();
        updateCamera();
        respawnPlayers();
    }

    void deathCheck()
    {
        int deathCount = 0;
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<PlayerMovement>().isDead)
            {
                deathCount++;
            }
        }
        Debug.Log(players.Length);
        if (deathCount == players.Length)
        {
            nextConnector.GetComponent<roomConnector>().Reset();
        }
    }
}
