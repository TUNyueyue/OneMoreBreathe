using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

#region
[Serializable]
public class Material
{
    public string name;
    public int quantity;

    public Material(string name, int num = 0)
    {
        this.name = name;
        this.quantity = num;
    }
}
#endregion
//Material¿‡

[CreateAssetMenu(fileName = "WarehouseSO", menuName = "Data/Warehouse")]
public class WarehouseSO : ScriptableObject
{
    [SerializeField] List<Material> materials = new List<Material>();


    public void Reset()
    {
        materials.Clear();
        materials.Add(new Material("Branch"));
        materials.Add(new Material("Gear"));
        materials.Add(new Material("Crystal"));
        materials.Add(new Material("Currency"));
    }
    public void ResetAllMaterials()
    {
        for (int i = 0; i < materials.Count; i++)
        {
            materials[i].quantity = 0;
        }
    }
    public void AddMaterialByName(string name, int num)
    {
        if (num == 0) return;
        for (int i = 0; i < materials.Count; i++)
        {
            if (materials[i].name == name)
            {
                materials[i].quantity += num;
                break;
            }
        }
    }
    public void RemoveMaterialByName(string name, int num)
    {
        if (num == 0)return;
        for (int i = 0; i < materials.Count; i++)
        {
            if (materials[i].name == name)
            {
                materials[i].quantity -= num;
                break;
            }
        }
    }

    public int GetQuantityByName(string name)
    {
        for (int i = 0; i < materials.Count; i++)
        {
            if (materials[i].name == name)
            {
                return materials[i].quantity;               
            }
        }
        return 0;
    }
}
