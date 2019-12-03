using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private PlayerMovement movement;
    private int controlToUse = -1;
    private bool usingController = false;
    private struct KeyboardMovement
    {
        public KeyCode Left;
        public KeyCode Right;
        public KeyCode Forwards;
        public KeyCode Backwards;
        public KeyCode Jump;
        public KeyCode Push;
    }
    private Gamepad playerGamepad;
    private KeyboardMovement keyboard;
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
        if (usingController)
        {
            Vector2 playerMovement = playerGamepad.leftStick.ReadValue();
            //Move left
            if (playerMovement.x < -0.2)
            {
                movement.MoveDirection(PlayerMovement.movementDirection.Left);
                //Debug.Log("Left");
            }

            //Move Right
            if (playerMovement.x > 0.2)
            {
                movement.MoveDirection(PlayerMovement.movementDirection.Right);
                //Debug.Log("Right");
            }

            //Move Up
            if (playerMovement.y > 0.2)
            {
                movement.MoveDirection(PlayerMovement.movementDirection.Forward);
                //Debug.Log("Forwards");
            }

            //Move Down
            if (playerMovement.y < -0.2)
            {
                movement.MoveDirection(PlayerMovement.movementDirection.Backward);
                //Debug.Log("Backwards");
            }

            //Jump
            if (playerGamepad.rightShoulder.isPressed)
            {
                movement.Jump();
            }

            if(playerGamepad.rightTrigger.isPressed)
            {
                movement.Push();
            }
        }
        else
        {
            //Move left
            if (Input.GetKey(keyboard.Left))
            {
                movement.MoveDirection(PlayerMovement.movementDirection.Left);
            }

            //Move Right
            if (Input.GetKey(keyboard.Right))
            {
                movement.MoveDirection(PlayerMovement.movementDirection.Right);
            }

            //Move Up
            if (Input.GetKey(keyboard.Forwards))
            {
                movement.MoveDirection(PlayerMovement.movementDirection.Forward);
            }

            //Move Down
            if (Input.GetKey(keyboard.Backwards))
            {
                movement.MoveDirection(PlayerMovement.movementDirection.Backward);
            }

            //Jump
            if (Input.GetKeyDown(keyboard.Jump))
            {
                movement.Jump();
            }

            if(Input.GetKeyDown(keyboard.Push))
            {
                movement.Push();
            }
        }
    }
    public void ControlMapping(int playerController)
    {
        switch (playerController)
        {
            case 0:
                keyboard.Left = KeyCode.A;
                keyboard.Right = KeyCode.D;
                keyboard.Forwards = KeyCode.W;
                keyboard.Backwards = KeyCode.S;
                keyboard.Push = KeyCode.Q;
                keyboard.Jump = KeyCode.E;
                break;
            case 1:
                keyboard.Left = KeyCode.J;
                keyboard.Right = KeyCode.L;
                keyboard.Forwards = KeyCode.I;
                keyboard.Backwards = KeyCode.K;
                keyboard.Push = KeyCode.U;
                keyboard.Jump = KeyCode.O;
                break;
            case 2:
                keyboard.Left = KeyCode.Delete;
                keyboard.Right = KeyCode.PageDown;
                keyboard.Forwards = KeyCode.Home;
                keyboard.Backwards = KeyCode.End;
                keyboard.Push = KeyCode.Insert;
                keyboard.Jump = KeyCode.PageUp;
                break;
            case 3:
                keyboard.Left = KeyCode.Keypad4;
                keyboard.Right = KeyCode.Keypad6;
                keyboard.Forwards = KeyCode.Keypad8;
                keyboard.Backwards = KeyCode.Keypad5;
                keyboard.Push = KeyCode.Keypad7;
                keyboard.Jump = KeyCode.Keypad9;
            break;
        }
        usingController = false;
        return;
    }
    public void ControlMapping(Gamepad gamePadToBeUsed)
    {
        playerGamepad = gamePadToBeUsed;
        usingController = true;
        return;
    }
}
