using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDigger : Digger
{
    LineRenderer line;
    Transform craftTransform;
    float laserLength=2f;
    int timer;
    public override void Dig()
    {
        line.positionCount = 2;
        EmitLaser(craftTransform.transform.up);
    }
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        craftTransform = GetComponentInParent<Transform>();
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //我愿称之为棍母代码，已加入Java赤石概念全家桶
        }
        else { line.positionCount = 0; }
    }

    void EmitLaser(Vector2 dir)
    {
        Vector2 startP = this.transform.position;     
        Vector2 endP = startP+dir*laserLength;

        

        RaycastHit2D hit =  Physics2D.Raycast(startP, dir, laserLength);      
        IMinable minableItem = hit.collider?.gameObject.GetComponent<IMinable>();
        if (minableItem != null)
        {
            timer++;
            if (timer > 10)
            {
                minableItem?.Consume(this.attack);
                timer = 0;
            }
            //采矿逻辑


            endP = startP + dir * hit.distance;
            hit.collider.gameObject.transform.localScale *= 0.99f;
            //显示反馈
        }

        line.SetPosition(0, startP);
        line.SetPosition(1, endP);
    }
}
