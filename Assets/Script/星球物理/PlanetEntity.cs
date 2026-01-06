using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetEntity : MonoBehaviour
{
    public float forceScale = 5f;
    public Rigidbody2D rb { get; private set; }
    public float planetRadius { get; private set; }
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void SetPlanetRadius(float planetRadius)
    {
        this.planetRadius = planetRadius;
    }
}
