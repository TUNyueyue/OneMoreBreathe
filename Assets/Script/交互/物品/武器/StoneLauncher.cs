using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneLauncher : Weapon
{
    [SerializeField] Stone stone;
    [SerializeField] float throwForce;


    public override void Use(Vector2 dir)
    {
        Stone stone = Instantiate(this.stone, this.transform.position, Quaternion.identity);
        stone.Init(dataSO.Attack, dataSO.Efficiency);
        stone.GetComponent<Rigidbody2D>().AddForce(dir * 100f * throwForce);
    }


    
}

