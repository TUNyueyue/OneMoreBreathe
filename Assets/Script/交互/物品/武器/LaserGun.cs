using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Weapon
{
    [SerializeField] LaserBullet bullet;
    [SerializeField] float bulletForce = 5f;
    void Awake()
    {
       
    }
    public override void Use(Vector2 dir)
    {
        LaserBullet bullet = Instantiate(this.bullet, this.transform.position, Quaternion.identity);
        bullet.Init(dataSO.Attack,dataSO.Efficiency);
        //≥ı ºªØ
        bullet.GetComponent<Rigidbody2D>().AddForce(dir * 100f * bulletForce);
    } 
    void Update()
    {
 
    }


}
