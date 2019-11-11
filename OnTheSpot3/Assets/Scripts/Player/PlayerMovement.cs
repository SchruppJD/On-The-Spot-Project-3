using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    private Vector3 direction;

    private Rigidbody myRigidBody;

    public enum movementDirection
    {
        Forward, Backward, Left, Right
    };

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        myRigidBody.AddForce(direction * movementSpeed);
        direction = Vector3.zero;
    }

    public void MoveDirection(movementDirection e)
    {
        switch(e)
        {
            case movementDirection.Forward:
                direction += new Vector3(0.0f, 0.0f, 1.0f);
                break;
            case movementDirection.Backward:
                direction += new Vector3(0.0f, 0.0f, -1.0f);
                break;
            case movementDirection.Left:
                direction += new Vector3(-1.0f, 0.0f, 0.0f);
                break;
            case movementDirection.Right:
                direction += new Vector3(1.0f, 0.0f, 1.0f);
                break;
            default:
                break;
        }
    }

    public void Jump()
    {
        direction += new Vector3(0.0f, 50.0f, 0.0f);
    }

    void KeepUpright()
    {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }
}
