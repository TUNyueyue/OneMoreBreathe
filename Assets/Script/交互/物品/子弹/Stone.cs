using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : PlanetEntity
{
    int attack;
    int efficiency;
    bool onTrigger = false;
    [SerializeField]float collisionTime = 0.5f;
    
    public void Init(int attack, int efficiency)
    {
        this.attack = attack;
        this.efficiency = efficiency;
        Destroy(this.gameObject, 10f);
    }
    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.GetComponent<PlanetPlayer>() || onTrigger) return;
        IAttackable attackable = collider.GetComponent<IAttackable>();
        if (attackable != null)
        {
            if (attackable.type == AttackedType.Character)
                attackable.OnAttack(attack);
            else
                attackable.OnAttack(efficiency);
        }
        Destroy(this.gameObject, collisionTime);
        onTrigger = true;
    }


}
