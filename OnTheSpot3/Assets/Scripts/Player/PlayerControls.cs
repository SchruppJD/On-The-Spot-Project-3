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
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movement.MoveDirection(PlayerMovement.movementDirection.Left);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            movement.MoveDirection(PlayerMovement.movementDirection.Right);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            movement.MoveDirection(PlayerMovement.movementDirection.Forward);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            movement.MoveDirection(PlayerMovement.movementDirection.Backward);
        }
    }
}
