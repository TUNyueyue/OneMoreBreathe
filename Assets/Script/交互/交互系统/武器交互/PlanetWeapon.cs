using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetWeapon : PlanetEntity, IInteractive
{
    public WeaponData data;

    [field: SerializeField] public GameObject Tip { get; set; }

    //孩子们差点忘了可以序列化

    protected override void Awake()
    {
        base.Awake();
        data.Init();
    }
    void Start()
    {
        //byd今天才发现Start里赋值会覆盖new出来的对象字段
    }

    void ReadDurability(int durability)
    {
        data.currentDurability = durability;
    }

    public void Interact(PlayerHand hand)
    {
        if (Inventory.instance.CheckEmptySlot() != null)
        {
            hand.GetWeapon(this);
            Destroy(this.gameObject);
        }
    }

}
