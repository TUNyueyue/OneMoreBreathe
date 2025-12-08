using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICamerable 
{
    event Action<Camera> onPull;
    void OnPull_Invoke(Camera camera);
}
