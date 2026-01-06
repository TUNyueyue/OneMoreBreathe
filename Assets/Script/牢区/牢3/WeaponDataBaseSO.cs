using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WeaponDataBaseSO", menuName = "Data/WeaponBaseData")]
public class WeaponDataBaseSO : ScriptableObject
{
   public List<WeaponDataSO> weaponDatas;

    public WeaponDataSO FindWeaponByName(string name)
    {
        for (int i = 0; i < weaponDatas.Count; i++)
        { 
        if (weaponDatas[i].name == name) return weaponDatas[i];      
        }
        return null;
    }
}
