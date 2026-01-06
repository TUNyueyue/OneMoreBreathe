using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class WeaponData
{    
    //开动脑筋了
    public WeaponDataSO metaData;
    public int currentDurability;

    public void Init()
    {
        currentDurability = metaData.Durability;
    }

    public WeaponData(WeaponDataSO metaData, int currentDurability)
    {
        this.metaData = metaData;
        this.currentDurability = metaData.Durability;
    }
    //暂且不知道这个构造函数有啥用。。。
}
