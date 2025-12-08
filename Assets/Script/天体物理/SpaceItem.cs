using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceItem : MonoBehaviour, IAttractable
{
    protected Rigidbody2D rb;
    [SerializeField] float orbitForceScale = 10f;
    [SerializeField] float outerLinerDrag = 5f;
    [SerializeField] float innerLinerDrag = 0f;
    Rigidbody2D IAttractable.rigidbody => rb;
    Transform IAttractable.transform => this.transform;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }
    public virtual void OnAttract(Attractor attractor)
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
