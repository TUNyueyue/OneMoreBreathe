using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    Rigidbody2D rigidBody;
    [SerializeField] float velocity = 5f;
    [SerializeField] float roationVelocity = 3f;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

    }


    void Update()
    {
        float Xinput = Input.GetAxisRaw("Horizontal");
        if (Xinput == 1)
        { rigidBody.angularVelocity -= roationVelocity; }
        else if (Xinput == -1)
        { rigidBody.angularVelocity += roationVelocity; }
        else
        {
            if (rigidBody.angularVelocity > 0)
                rigidBody.angularVelocity -= roationVelocity;
            else if (rigidBody.angularVelocity < 0)
                rigidBody.angularVelocity += roationVelocity;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.velocity = transform.up * velocity * 3f;
        }
        else            
            rigidBody.velocity = transform.up * velocity;
    }
}
