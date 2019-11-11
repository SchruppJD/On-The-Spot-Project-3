using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorAssign : MonoBehaviour
{
    public Material[] playerColors;
    // Start is called before the first frame update
    void Start()
    {
        //AssignColor();
        AssignColorRandom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Assigns the color to the player based on what they picked in the menu
    void AssignColor()
    {

    }

    //temporarily use until menus are made
    void AssignColorRandom()
    {
        int randomNum = Random.Range(0, playerColors.Length - 1);
        GetComponent<Renderer>().material = playerColors[randomNum];
    }
}
