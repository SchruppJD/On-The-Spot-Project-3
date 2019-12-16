using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> traps = new List<GameObject>();
    public GameObject[] players;

    void Start()
    {
        Physics.gravity = new Vector3(0, -19.6f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
