using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKill : MonoBehaviour
{
    ParticleSystem ps;
    public bool isMoving = false;
    public float movementRate;
    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }
    private void FixedUpdate()
    {
        if (isMoving)
        {
            gameObject.transform.Translate(movementRate/10, 0f,0f);
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Player" && !other.GetComponent<PlayerMovement>().isDummy)
        {
            other.GetComponent<PlayerMovement>().Kill();
        }
    }
    public void ChangeSize(float sizeChange)
    {
        ParticleSystem.ShapeModule newPs = ps.shape;
        newPs.scale = new Vector3(newPs.scale.x, sizeChange , newPs.scale.z);
    }
}
