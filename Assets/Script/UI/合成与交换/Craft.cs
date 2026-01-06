using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft : MonoBehaviour
{
    [SerializeField] WeaponData data;
    [SerializeField] Transform initTrans;
    public void GetWeapon()
    {
        var anySolt = Inventory.instance.CheckEmptySlot();
        if (anySolt != null)
        {
            Inventory.instance.GetWeapon(data);
        }
        else
        {
            var weapon = WeaponFactory.instance.GetPlanetWeapon(data);
            Instantiate(weapon, initTrans.position, Quaternion.identity);
        }

    }
}
