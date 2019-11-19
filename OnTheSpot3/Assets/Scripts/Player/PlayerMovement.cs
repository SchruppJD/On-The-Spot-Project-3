using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject pushBox;
    public float movementSpeed;
    private Vector3 direction;

    private Rigidbody myRigidBody;

    public bool isDead = false;

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
        myRigidBody.MovePosition(transform.position + direction * movementSpeed * Time.deltaTime);
        direction = Vector3.zero;
    }

    public void MoveDirection(movementDirection e)
    {
        if (isDead)
            return;
        Quaternion targetRotation = transform.rotation;
        switch(e)
        {
            case movementDirection.Forward:
                direction += new Vector3(0.0f, 0.0f, 1.0f);
                targetRotation = Quaternion.Euler(0, 0, 0);
                break;
            case movementDirection.Backward:
                direction += new Vector3(0.0f, 0.0f, -1.0f);
                targetRotation = Quaternion.Euler(0, -180, 0);
                break;
            case movementDirection.Left:
                direction += new Vector3(-1.0f, 0.0f, 0.0f);
                targetRotation = Quaternion.Euler(0, -90, 0);
                break;
            case movementDirection.Right:
                direction += new Vector3(1.0f, 0.0f, 0.0f);
                targetRotation = Quaternion.Euler(0, 90, 0);
                break;
            default:
                break;
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, .1f);
    }

    public void Jump()
    {
        direction += new Vector3(0.0f, 20.0f, 0.0f);
    }

    void KeepUpright()
    {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

    public void Kill()
    {
        myRigidBody.constraints = RigidbodyConstraints.None;
        isDead = true;
    }

    public void Push()
    {
        pushBox.GetComponent<BoxCollider>().enabled = !pushBox.GetComponent<BoxCollider>().enabled;
    }
}
