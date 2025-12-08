using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour,IPickable
{
    public void OnPickUp()
    {
         Destroy(this.gameObject);
    }

    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
}
