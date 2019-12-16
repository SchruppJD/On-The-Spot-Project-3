﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    private float timer;
    private bool gameStarted = false;
    private Transform camera;

    private void Start()
    {
        timer = 0;
        camera = Camera.main.transform;
    }

    private void Update()
    {
        if(gameStarted)
        {
            //timer is up
            if (timer > 3.6f)
            {
                camera.GetChild(0).gameObject.SetActive(false);
                camera.GetChild(1).gameObject.SetActive(false);
                camera.GetChild(2).gameObject.SetActive(false);
                //GameObject.Find("RoomManager").GetComponent<roomManager>().changeRoom();
                Destroy(GetComponent<GameStart>());
            }
            //timer has one second left
            else if (timer > 2.2f)
            {
                camera.GetChild(0).gameObject.SetActive(false);
                camera.GetChild(1).gameObject.SetActive(false);
                camera.GetChild(2).gameObject.SetActive(true);
            }
            //timer has two seconds left
            else if(timer > 1)
            {
                camera.GetChild(0).gameObject.SetActive(false);
                camera.GetChild(1).gameObject.SetActive(true);
                camera.GetChild(2).gameObject.SetActive(false);
            }
            //timer just started
            else
            {
                camera.GetChild(0).gameObject.SetActive(true);
                camera.GetChild(1).gameObject.SetActive(false);
                camera.GetChild(2).gameObject.SetActive(false);
            }
            timer += Time.deltaTime;
        }
    }

    public void StartGame()
    {
        gameStarted = true;
    }
}
