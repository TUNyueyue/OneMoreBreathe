using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class L_Planet : L_Attractor
{
    float escapeDistance = 5f;
    void Awake()
    {
        itemsInForce = new HashSet<L_IAttractable>();

    }
    void Start()
    {

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, radius + escapeDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, radius);

    }
    void FixedUpdate()
    {
        DetectItems();
        ApplyForce();
    }

    private void DetectOut()
    {
        itemsInForce.RemoveWhere(attractedItem =>
        {

            if (attractedItem == null) return true;
            float distance = Vector2.Distance(this.transform.position, attractedItem.transform.position);
            if (distance > this.radius + escapeDistance)
            //设置逃逸范围防止边界频繁进出
            {
                attractedItem?.OutAttract();
                //OutAttract回调
                return true;
            }
            return false;
        }
                   );
    }

    private void DetectIn()
    {
        itemsDetected = Physics2D.OverlapCircleAll(this.transform.position, this.radius);
        foreach (Collider2D collider in itemsDetected)
        {
            if (collider.gameObject == this.gameObject)
                continue;
            L_IAttractable attractedItem = collider.GetComponent<L_IAttractable>();
            if (!itemsInForce.Contains(attractedItem) && attractedItem != null)
            {
                itemsInForce.Add(collider.GetComponent<L_IAttractable>());
                collider.GetComponent<L_IAttractable>()?.OnAttract(this);
                //OnAttract回调
            }

        }
    }

    override protected void DetectItems()
    {
        DetectOut();
        DetectIn();
        //先检测离开的,避免同一帧即进入又离开(?
    }
    override protected void ApplyForce()
    {

        foreach (L_IAttractable attractedItem in itemsInForce)
        {
            if (attractedItem != null)
            {
                Rigidbody2D rb = attractedItem.rigidbody;
                Vector2 direction = (this.transform.position - attractedItem.transform.position).normalized;
                float distance = Vector2.Distance(this.transform.position,attractedItem.transform.position);
                rb.AddForce(direction * forceScale*distance*0.1f);
            }
        }
    }
}
