using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class WeaponFactory : MonoBehaviour
{
    [SerializeField] PlanetWeapon[] planetWeapons;//GameObject数组
    public static WeaponFactory instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public PlanetWeapon GetPlanetWeapon(WeaponData data)
    {
        if (data == null) return null;
        for (int i = 0; i < planetWeapons.Length; i++)
        {
            if (planetWeapons[i].data.metaData.WeaponName == data.metaData.WeaponName)
            //小知识点，虽然string是引用类型，但也可以判断值，因为这里的==方法重载了
            {
                planetWeapons[i].data.currentDurability = data.currentDurability;
                return planetWeapons[i];
            }

        }
        return null;
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
