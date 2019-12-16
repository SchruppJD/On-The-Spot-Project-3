using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerText : MonoBehaviour
{
    public GameObject Parent;
    string text;
    GameObject[] players;
    private void Start()
    {
        gameObject.GetComponent<TextMeshPro>().color = Parent.GetComponentInParent<Renderer>().material.color;

        Invoke("SetNames", 0.1f);
    }

    void Update()
    {
        transform.LookAt(new Vector3(gameObject.transform.position.x, 0, Camera.main.transform.position.z));
        transform.localEulerAngles += new Vector3(60, 180, 0);

        if(gameObject.GetComponentInParent<PlayerMovement>().isDead)
        {
            Destroy(gameObject);
        }
    }

    void SetNames()
    {
        players = GameObject.FindGameObjectWithTag("GameController").GetComponent<roomManager>().players;
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].transform.position.z == Parent.transform.position.z)
            {
                text = "Player " + (i + 1);
            }
        }

        gameObject.GetComponent<TextMeshPro>().text = text;
    }
}
