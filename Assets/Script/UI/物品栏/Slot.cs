using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    Image slotImage;
    [SerializeField] Image iconImage;
    [SerializeField] Sprite emptyIcon;
    //只存数据
    public WeaponData currentWeapon { get; private set; }
    public bool isEmpty = true;

    void Awake()
    {
        slotImage = GetComponent<Image>();
    }
    public void SetWeapon(WeaponData weapon)
    {
        currentWeapon = weapon;
        iconImage.sprite = currentWeapon.metaData.Icon;
        isEmpty = false;
    }
    public void SetEmpty()
    {
        currentWeapon = null;
        iconImage.sprite = emptyIcon;
        isEmpty = true;
    }
    public void OnSelect()
    {
        slotImage.color = new Color(255 / 255f, 110 / 255f, 110 / 255f, slotImage.color.a);//淡红色
        //Debug.Log(this.gameObject.name);
    }
    public void OutSelect()
    {
        slotImage.color = new Color(226 / 255f, 226 / 255f, 226 / 255f, slotImage.color.a);//默认颜色
    }
}
