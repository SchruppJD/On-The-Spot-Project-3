using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    public Canvas currentC;
    public Text currentTitle;
    public int numberOfPlayers;
    private int playersAssigned = 0;
    public GameObject[] playersCreated;
    public int numberOfControllers;
    public GameObject characters;
    private int startingX = 0;
    // Start is called before the first frame update
    void Start()
    {
        numberOfControllers = 0;
    }
    public void SetPlayerNumber(int number)
    {
        playersCreated = new GameObject[number];
        numberOfPlayers = number;
        for(int i = 0; i < number; i++)
        {
            playersCreated[i] = Instantiate(characters, new Vector3(startingX, 2, 0), Quaternion.identity);
            startingX += 2;
        }
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().players = playersCreated;
    }
    public void SetupControls(bool isUsingController)
    {
        playersCreated[playersAssigned].GetComponent<PlayerControls>().ControlMapping(playersAssigned, isUsingController);
        playersAssigned++;
        currentTitle.GetComponent<Text>().text = "What controls will Player " + (playersAssigned + 1).ToString() + " use?";

        if (playersAssigned == numberOfPlayers)
        {
            currentC.gameObject.SetActive(false);
        }

    }
}
