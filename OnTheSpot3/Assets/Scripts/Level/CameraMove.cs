using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().players.Length > 0)
        {
            gameObject.transform.position = Vector3.Lerp(transform.position, CenterOfPlayers(GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().players), .1f);
        }
            
    }

    Vector3 CenterOfPlayers(GameObject[] players)
    {
        float x = 0;
        float z = 0;
        for(int i = 0; i < players.Length; i++)
        {
            x += players[i].transform.position.x;
            z += players[i].transform.position.z;
        }

        x /= players.Length;
        z /= players.Length;

        return new Vector3(x, gameObject.transform.position.y, z - 10);
    }
}
