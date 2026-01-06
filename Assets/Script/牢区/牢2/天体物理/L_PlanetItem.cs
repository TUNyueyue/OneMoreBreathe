using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_PlanetItem : MonoBehaviour, L_IAttractable
{
    protected Rigidbody2D rb;
    [SerializeField] float orbitForceScale = 10f;
    [SerializeField] float outerLinerDrag = 5f;
    [SerializeField] float innerLinerDrag = 0f;
    Rigidbody2D L_IAttractable.rigidbody => rb;
    Transform L_IAttractable.transform => this.transform;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }
    public virtual void OnAttract(L_Attractor attractor)
    {
        Debug.Log("OnAttract!");
        rb.drag = innerLinerDrag;
        Vector2 dir = attractor.transform.position - this.transform.position;
        rb.AddForce(new Vector2(dir.y, -dir.x) * orbitForceScale);//求垂直向量
    }

    public virtual void OutAttract()
    {
        rb.drag = outerLinerDrag;
    }
}
