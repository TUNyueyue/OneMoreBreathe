using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jet : MonoBehaviour
{
    Rigidbody2D rigidBody;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        
    }

    IEnumerator JetTheRocket()
    {Vector2 tempVelocity= rigidBody.velocity;
        rigidBody.velocity *= 3;
    yield return new WaitForSeconds(1f);



    }
}
