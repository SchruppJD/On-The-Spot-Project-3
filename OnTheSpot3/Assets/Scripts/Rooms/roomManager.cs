using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class roomManager : MonoBehaviour
{
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

    int[] playerPoints;
    // Start is called before the first frame update
    void Start()
    {
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
        activeRoom = Instantiate(activeRoom, new Vector3((startRoomWidth/2) + (activeRoomWidth/2),0,0), Quaternion.identity);

        nextConnector = Instantiate(connectorRoom, new Vector3((activeRoom.transform.position.x) + (activeRoomWidth / 2) + (connectorRoomWidth / 2), 0, 0), Quaternion.identity);

        updateCamera();

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


    void newRoom(){
        int newRoomIndex;
        do
        {
            newRoomIndex = Random.Range(0, roomPrefabs.Length);
        } while (roomIndex == newRoomIndex);
        roomIndex = newRoomIndex;
        nextRoom = roomPrefabs[roomIndex];
        nextRoomWidth = nextRoom.GetComponent<BoxCollider>().size.x;
        float newPositionX = activeRoom.transform.position.x + (activeRoomWidth/2) + connectorRoomWidth + (nextRoomWidth/2);
        //Debug.Log(connectorRoom.GetComponent<BoxCollider>().size.x);
        nextRoom = Instantiate(nextRoom, new Vector3(newPositionX, 0, 0), Quaternion.identity);
        Destroy(activeRoom, 0.1f);
        
        activeRoom = nextRoom;

        newConnector();
    }

    void newConnector(){
        Destroy(activeConnector, 1f);
        activeConnector = nextConnector;
        nextConnector = Instantiate(connectorRoom, new Vector3( (activeRoom.transform.position.x) + (activeRoomWidth/2) + (connectorRoomWidth/2)      ,0,0), Quaternion.identity);
    }

    void respawnPlayers(){
        for (int i = 0; i < players.Length; i++)
        {
            //Debug.Log(players.Length);
            //Debug.Log(players[i].transform.position.y);
            //if(players[i].transform.position.y < 0)
            //{
           players[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
           players[i].transform.position = new Vector3(activeConnector.transform.position.x, 1.5f, (activeConnector.transform.position.z - 5) + (3*i)  );
           players[i].GetComponent<PlayerMovement>().isDead = false;
           players[i].transform.rotation = Quaternion.Euler(0, 90, 0);
           players[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            //}
        }
    }
     
    void updateCamera(){
        //Debug.Log(activeRoom.transform.position.x);
        camera.transform.position = new Vector3(activeRoom.transform.position.x, camera.transform.position.y, camera.transform.position.z);
    }

    public void playerFinished(GameObject player)
    {
        for (int i = 0; i < playerPoints.Length; i++)
        {
            if(player == players[i])
            {
                playerPoints[i]++;
                
            }
        }
    }

    public void changeRoom()
    {
        newRoom();
        updateCamera();
        respawnPlayers();

        Debug.Log("Current Points: ");
        for (int i = 0; i < players.Length; i++)
        {
            Debug.Log("Player" + (i + 1) + ": " + playerPoints[i]);
        }
    }

    void deathCheck()
    {
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
            nextConnector.GetComponent<roomConnector>().Reset();
        }
    }
}
