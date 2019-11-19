using System.Collections;
using System.Collections.Generic;
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
    }
    private struct JoystickMovement
    {
        public string Left;
        public string Right;
        public string Forwards;
        public string Backwards;
        public string Jump;
    }
    private KeyboardMovement keyboard;
    private JoystickMovement joystick;
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
            
            //Move left
            if (Input.GetButton(joystick.Left))
            {
                movement.MoveDirection(PlayerMovement.movementDirection.Left);
                Debug.Log("Left");
            }

            //Move Right
            if (Input.GetButton(joystick.Right))
            {
                movement.MoveDirection(PlayerMovement.movementDirection.Right);
                Debug.Log("Right");
            }

            //Move Up
            if (Input.GetButton(joystick.Forwards))
            {
                movement.MoveDirection(PlayerMovement.movementDirection.Forward);
                Debug.Log("Forwards");
            }

            //Move Down
            if (Input.GetButton(joystick.Backwards))
            {
                movement.MoveDirection(PlayerMovement.movementDirection.Backward);
                Debug.Log("Backwards");
            }

            //Jump
            if (Input.GetButton(joystick.Jump))
            {
                movement.Jump();
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
        }
    }
    public void ControlMapping(int playerController,bool isUsingController)
    {
        if (isUsingController)
        {
            usingController = true;
            GameObject manager = GameObject.Find("IfaceManager");
            int currentlyUsedControllers = manager.GetComponent<InterfaceManager>().numberOfControllers;
            if(currentlyUsedControllers != Input.GetJoystickNames().Length)
            {
                switch (currentlyUsedControllers)
                {
                    case 0:
                        joystick.Left = "Left_P1";
                        joystick.Right = "Right_P1";
                        joystick.Forwards = "Forward_P1";
                        joystick.Backwards = "Backward_P1";
                        joystick.Jump = "Jump_P1";
                        break;
                    case 1:
                        joystick.Left = "Left_P2";
                        joystick.Right = "Right_P2";
                        joystick.Forwards = "Forward_P2";
                        joystick.Backwards = "Backward_P2";
                        joystick.Jump = "Jump_P2";
                        break;
                    case 2:
                        joystick.Left = "Left_P3";
                        joystick.Right = "Right_P3";
                        joystick.Forwards = "Forward_P3";
                        joystick.Backwards = "Backward_P3";
                        joystick.Jump = "Jump_P3";
                        break;
                    case 3:
                        joystick.Left = "Left_P4";
                        joystick.Right = "Right_P4";
                        joystick.Forwards = "Forward_P4";
                        joystick.Backwards = "Backward_P4";
                        joystick.Jump = "Jump_P4";
                        break;
                }
                manager.GetComponent<InterfaceManager>().numberOfControllers++;
            }
            return;
        }
        switch (playerController)
        {
            case 0:
                keyboard.Left = KeyCode.A;
                keyboard.Right = KeyCode.D;
                keyboard.Forwards = KeyCode.W;
                keyboard.Backwards = KeyCode.S;

                break;
            case 1:
                keyboard.Left = KeyCode.J;
                keyboard.Right = KeyCode.L;
                keyboard.Forwards = KeyCode.I;
                keyboard.Backwards = KeyCode.K;

                break;
            case 2:
                keyboard.Left = KeyCode.LeftArrow;
                keyboard.Right = KeyCode.RightArrow;
                keyboard.Forwards = KeyCode.UpArrow;
                keyboard.Backwards = KeyCode.DownArrow;

                break;
            case 3:
                keyboard.Left = KeyCode.Keypad4;
                keyboard.Right = KeyCode.Keypad6;
                keyboard.Forwards = KeyCode.Keypad8;
                keyboard.Backwards = KeyCode.Keypad2;
            break;
        }
        usingController = false;
        return;
    }
}
