using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCamera : MonoBehaviour
{
    public ICamerable subject;
    new Camera camera;
    bool isGrounded;
    void Start()
    {
        camera = GetComponent<Camera>(); 
    }


    void Update()
    {
        subject.OnPull_Invoke(this.camera);
    }
}
