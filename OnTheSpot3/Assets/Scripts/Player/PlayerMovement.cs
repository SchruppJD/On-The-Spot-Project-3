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
    public bool isDummy = false;

    private float pushTimer = 0.1f;

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
        // check for pushbox
        if (pushBox.GetComponent<BoxCollider>().enabled)
        {
            pushTimer -= Time.deltaTime;
            if (pushTimer < 0.0f)
            {
                pushBox.GetComponent<BoxCollider>().enabled = false;
            }
        }

        // create transparency
        if (isDead)
        {
            Material baseMat = GetComponent<Renderer>().material;
            baseMat.color = new Color(baseMat.color.r, baseMat.color.g, baseMat.color.b, 0.5f);
        }
        MoveCharacter();
    }

    void MoveCharacter()
    {
        myRigidBody.MovePosition(transform.position + direction * movementSpeed * Time.deltaTime);
        direction = Vector3.zero;
    }

    public void MoveDirection(movementDirection e)
    {
        if (isDummy)
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
        if (!isDead)
        {
            myRigidBody.constraints = RigidbodyConstraints.None;
            myRigidBody.mass = 0.5f;
            GameObject dummy = Instantiate(gameObject, transform.position, Quaternion.identity);
            dummy.GetComponent<PlayerMovement>().isDummy = true;
            dummy.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
            gameObject.layer = 9;
            foreach (Transform child in transform)
            {
                // pushing still works
                if (child.name != "PushBox")
                {
                    child.gameObject.layer = 9;
                }
            }
            // ignore trap collision
            Physics.IgnoreLayerCollision(8, 9);
            // ignore player collision
            Physics.IgnoreLayerCollision(9, 10);
            transform.position = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
            isDead = true;
        }
    }

    public void Push()
    {
        pushTimer = 0.1f;
        pushBox.GetComponent<BoxCollider>().enabled = true;
    }
}
