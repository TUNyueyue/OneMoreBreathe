using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour //牢区代码删掉了出入回调
{

    [SerializeField] float forceRadius;
    [SerializeField] float radius;
    HashSet<PlanetEntity> itemsInForce;
    Collider2D[] itemsDetected;
    public bool hasBreath;
    void Awake()
    {
        itemsInForce = new HashSet<PlanetEntity>();
    }
    void Start()
    {
        DetectIn();
        //CircleCollider2D collider = GetComponent<CircleCollider2D>();
        //radius = collider.radius * 7.52f;实验失败了不是线性的，只能手调了
    }
    void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, forceRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
    void FixedUpdate()
    {
        ApplyForce();
        DetectIn();
    }

    void DetectIn()
    {
        itemsDetected = Physics2D.OverlapCircleAll(this.transform.position, this.forceRadius);
        foreach (Collider2D collider in itemsDetected)
        {
            if (collider.gameObject == this.gameObject)
                continue;
            PlanetEntity attractedItem = collider.GetComponent<PlanetEntity>();
            if (!itemsInForce.Contains(attractedItem) && attractedItem != null)
            {
                attractedItem.SetPlanetRadius(radius);
                itemsInForce.Add(collider.GetComponent<PlanetEntity>());
            }
        }
    }

    public void RemoveEntityInForce(PlanetEntity entity)
    {
        itemsInForce.Remove(entity);
    }

    void ApplyForce()
    {

        foreach (PlanetEntity attractedItem in itemsInForce)
        {
            if (attractedItem != null)
            {
                Rigidbody2D rb = attractedItem.rb;
                Vector2 direction = (this.transform.position - attractedItem.transform.position).normalized;
                float distance = Vector2.Distance(this.transform.position, attractedItem.transform.position);
                rb.AddForce(direction * attractedItem.forceScale * distance * 0.1f);
            }
        }
    }
}
