using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : Weapon
{
    [SerializeField] DigFlame flame;
    [SerializeField] float attackDistance = 1f;
    [SerializeField] LayerMask attackableLayer;

    public override void Use(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, this.transform.up, attackDistance, attackableLayer);
        bool hasHit = hit;
        if (hasHit)
        {
            Instantiate(flame, hit.point, Quaternion.identity);
            LaserBullet bullet = hit.collider.GetComponent<LaserBullet>();
            if (bullet != null)
            {
                var bulletRb = bullet.GetComponent<Rigidbody2D>();
                bulletRb.velocity = -bulletRb.velocity;
            }
            //µ¯·´×Óµ¯
            IAttackable attackable = hit.collider.GetComponent<IAttackable>();
            if (attackable != null)
            {
                if (attackable.type == AttackedType.Character)
                    attackable.OnAttack(dataSO.Attack);
                else
                    attackable.OnAttack(dataSO.Efficiency);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 endP = this.transform.position + this.transform.up * attackDistance;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(this.transform.position, endP);
    }

}
