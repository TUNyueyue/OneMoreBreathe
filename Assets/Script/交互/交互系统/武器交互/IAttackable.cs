using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AttackedType { Ore,Character }
public interface IAttackable
{  
    AttackedType type { get; set; }
    void OnAttack(int damage);
}
