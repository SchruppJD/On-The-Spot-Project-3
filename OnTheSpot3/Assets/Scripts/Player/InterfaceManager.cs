using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    public Canvas currentC;
    private List<Gamepad> assignedGamepads;
    public Text currentTitle;
    public Text controllerText;
    public int numberOfPlayers;
    private int playersAssigned = 0;
    public GameObject[] playersCreated;
    public int numberOfControllers;
    public GameObject characters;
    private int startingZ = 0;
    // Start is called before the first frame update
    void Start()
    {
        numberOfControllers = 0;
        assignedGamepads = new List<Gamepad>();

    }
    void FixedUpdate()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
            return; // No gamepad connected.
        if (controllerText.gameObject.activeSelf == false)
            return;
        if (gamepad.rightTrigger.isPressed)
        {
            ControllerSetup(gamepad);
        }
    }
    public void SetPlayerNumber(int number)
    {
        playersCreated = new GameObject[number];
        numberOfPlayers = number;
        for(int i = 0; i < number; i++)
        {
            playersCreated[i] = Instantiate(characters, new Vector3(0, 2, startingZ), Quaternion.identity);
            startingZ += 2;
        }
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().players = playersCreated;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<roomManager>().enabled = true;
    }
    public void DisplayMenu()
    {
        currentTitle.GetComponent<Text>().text = "What controls will Player " + (playersAssigned + 1).ToString() + " use?";
        if (playersAssigned == numberOfPlayers)
        {
            currentC.gameObject.SetActive(false);
        }
        controllerText.GetComponent<Text>().text = "If Player " + (playersAssigned + 1).ToString() + " is using a controller press right trigger on the controller he is using";
    }
    public void KeyboardSetup()
    {
        playersCreated[playersAssigned].GetComponent<PlayerControls>().ControlMapping(playersAssigned);
        playersAssigned++;
        DisplayMenu();
    }
    public void ControllerSetup(Gamepad currentGPad)
    {
        foreach(Gamepad ownedGamepads in assignedGamepads)
        {
            if(ownedGamepads == currentGPad)
            {
                controllerText.GetComponent<Text>().text = "The controller pressed is already owned by another player. Please try another controller or use the keyboard option.";
                return;
            }
        }
        playersCreated[playersAssigned].GetComponent<PlayerControls>().ControlMapping(currentGPad);
        assignedGamepads.Add(currentGPad);
        playersAssigned++;
        DisplayMenu();
    }
}
