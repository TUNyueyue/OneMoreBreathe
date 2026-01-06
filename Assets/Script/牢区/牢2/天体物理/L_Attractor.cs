using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class L_Attractor : MonoBehaviour
{
    [SerializeField]protected float forceScale;
    [SerializeField]protected float radius;
    protected HashSet<L_IAttractable> itemsInForce;
    protected Collider2D[] itemsDetected;

    protected abstract void DetectItems();
    protected abstract void ApplyForce();
  

}
