using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region
[Serializable]
struct DropItem
{
    public PlanetMaterial objectToDrop;
    public int num;
}
#endregion
//Ê²Ã´×½ÃÔ²Ø´óÈü
public class PlanetMaterial : PlanetEntity
{
    [SerializeField]new string name;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlanetPlayer>() != null)
        {
            Warehouse.instance.AddMaterialByName(this.name);
            Destroy(gameObject);
        }
    }

}
