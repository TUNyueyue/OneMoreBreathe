using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    //武器不需要现有耐久!!!
    public WeaponDataSO dataSO;

    public abstract void Use(Vector2 dir);

    //Drop位置存疑，按理说只要调用到weapon的rb就行
    //不是不是，这是手上的，要setFalse的，应该生成一个影子对象，也调用SO
    //然后这个影子对象有个耐久值
    //public void Drop()
    //{

    //}

 
}
