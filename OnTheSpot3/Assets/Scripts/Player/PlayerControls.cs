using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private PlayerMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        //Move left
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            movement.MoveDirection(PlayerMovement.movementDirection.Left);
        }

        //Move Right
        if(Input.GetKey(KeyCode.RightArrow))
        {
            movement.MoveDirection(PlayerMovement.movementDirection.Right);
        }

        //Move Up
        if (Input.GetKey(KeyCode.UpArrow))
        {
            movement.MoveDirection(PlayerMovement.movementDirection.Forward);
        }

        //Move Down
        if (Input.GetKey(KeyCode.DownArrow))
        {
            movement.MoveDirection(PlayerMovement.movementDirection.Backward);
        }

        //Jump
        if(Input.GetKeyDown(KeyCode.Space))
        {
            movement.Jump();
        }
    }
}
