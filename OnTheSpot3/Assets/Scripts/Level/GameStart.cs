using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    private float timer;
    private bool gameStarted = false;

    private void Awake()
    {
        timer = 0;
    }

    private void Update()
    {
        if(gameStarted)
        {
            if (timer > 3)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(false);
                Destroy(GetComponent<GameStart>());
            }
            else if (timer > 2)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                Debug.Log("1");
            }
            else if(timer > 1)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(2).gameObject.SetActive(false);
                Debug.Log("2");
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(false);
                Debug.Log("3");
            }
            timer += Time.deltaTime;
        }
    }

    public void StartGame()
    {
        gameStarted = true;
    }
}
