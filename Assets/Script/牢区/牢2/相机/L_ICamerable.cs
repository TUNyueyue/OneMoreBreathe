using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface L_ICamerable 
{
    event Action<Camera> onPull;
    void OnPull_Invoke(Camera camera);
}
