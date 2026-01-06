using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface L_IAttractable 
{
    Transform transform { get; } 
    Rigidbody2D rigidbody { get; }
    void OnAttract(L_Attractor attractor);
    void OutAttract();
}
