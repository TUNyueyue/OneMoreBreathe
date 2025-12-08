using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttractable 
{
    Transform transform { get; } 
    Rigidbody2D rigidbody { get; }
    void OnAttract(Attractor attractor);
    void OutAttract();
}
