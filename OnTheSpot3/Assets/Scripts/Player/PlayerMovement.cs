using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    private Vector3 direction;

    private Rigidbody myRigidBody;

    public bool isDead = false;
    public bool isDummy = false;

    // this is the player who most recently pushed this player
    public GameObject lastPusher;

    // player who pushed has 1.5 seconds until their push no longer counts
    private float pusherTimer = 1.5f;

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
        pusherTimer -= Time.deltaTime;
        // pusher no longer counts
        if (pusherTimer < 0)
        {
            lastPusher = null;
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
        switch (e)
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
                child.gameObject.layer = 9;
            }
            // ignore trap collision
            Physics.IgnoreLayerCollision(8, 9);
            // ignore player collision
            Physics.IgnoreLayerCollision(9, 10);
            // ignore ghost on ghost collision
            Physics.IgnoreLayerCollision(9, 9);
            transform.position = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
            isDead = true;


            // give pusher credit
            if (lastPusher != null)
            {
                // respawn pusher
                if (lastPusher.GetComponent<PlayerMovement>().isDead)
                {
                    lastPusher.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    lastPusher.transform.position = new Vector3(lastPusher.transform.position.x, lastPusher.transform.position.y + 3, lastPusher.transform.position.z);
                    lastPusher.GetComponent<PlayerMovement>().isDead = false;
                    lastPusher.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    Material baseMat = lastPusher.GetComponent<Renderer>().material;
                    baseMat.color = new Color(baseMat.color.r, baseMat.color.g, baseMat.color.b, 1.0f);
                    lastPusher.layer = 10;
                    foreach (Transform child in lastPusher.transform)
                    {
                        child.gameObject.layer = 10;
                    }
                }
            }
        }
    }

    public void Push()
    {
        // first find the player to push
        List<GameObject> players = new List<GameObject>();
        players.AddRange(GameObject.FindGameObjectsWithTag("Player"));

        RaycastHit pushRayHit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out pushRayHit, 1.5f))
        {
            foreach (GameObject player in players)
            {
                // player doesn't check against itself
                if (player == gameObject)
                {
                    continue;
                }

                // hit a living player
                if (player == pushRayHit.transform.gameObject && !player.GetComponent<PlayerMovement>().isDead)
                {
                    player.GetComponent<PlayerMovement>().lastPusher = gameObject;
                    player.GetComponent<PlayerMovement>().pusherTimer = 1.5f;
                    player.GetComponent<Rigidbody>().AddForce(transform.forward * 300);
                }
            }
        }

    }
}
