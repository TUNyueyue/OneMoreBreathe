using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    //手只需要能射击的武器而不是数据！
    //致敬传奇耦合王PlayerHand，能用武器能交互，还能扔东西，又是E_Input
    [Header("Weapon")]
    [SerializeField] float handLengh = 1f;
    [SerializeField] List<Weapon> weapons;
    Weapon currentWeapon;
    public static event Action OnUseWeapon;//又是连锁事件。。。
    [Header("Interact")]
    [SerializeField] float detectRadius = 1f;
    IInteractive currentInteractive;
    public static event Action OnGetKeyDownE;

    void OnEnable()
    {
        WeaponInput.OnClickMouse += UseWeapon;
        Inventory.OnSelectEvent += UpdateHand;
        InventoryInput.OnGetKeyDownQ += DropWeapon;
        PlayerHand.OnGetKeyDownE += TryInteract;
    }
    void OnDisable()
    {
        WeaponInput.OnClickMouse -= UseWeapon;
        Inventory.OnSelectEvent -= UpdateHand;
        InventoryInput.OnGetKeyDownQ -= DropWeapon;
        PlayerHand.OnGetKeyDownE -= TryInteract;
    }
    void Start()
    {

    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
    void UpdateHand()
    {
        SwitchToWeapon(Inventory.instance.currentSlot.currentWeapon);
    }
    void SwitchToWeapon(WeaponData data)
    {
        currentWeapon?.gameObject.SetActive(false);//初始不存在所以判断null
        if (data == null)
        {
            currentWeapon = null;
            return;
        }
        foreach (var weapon in weapons)
        {
            if (weapon.dataSO.WeaponName == data.metaData.WeaponName)
            {
                currentWeapon = weapon;
                break;
            }
        }
        currentWeapon?.gameObject.SetActive(true);
    }
    void SwitchToEmpty()
    {
        currentWeapon.gameObject.SetActive(false);
    }
    void Update()
    {
        SetWeaponDirection();
        DetectNearestInteractive();
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnGetKeyDownE.Invoke();
        }
    }

    void SetWeaponDirection()
    {
        if (currentWeapon != null)
        {
            currentWeapon.transform.position = new Vector2(this.transform.position.x + WeaponInput.vectorToWeapon.x* handLengh, this.transform.position.y + WeaponInput.vectorToWeapon.y* handLengh);
            currentWeapon.transform.up = -(this.transform.position - currentWeapon.transform.position).normalized;
        }
    }

    void UseWeapon()
    {
        if (currentWeapon != null)
        {
            currentWeapon.Use(WeaponInput.vectorToWeapon);
            WearWeapon();//没想到把，我在这里
            OnUseWeapon.Invoke();
        }
    }

    public void GetWeapon(PlanetWeapon planetWeapon)
    {
        Inventory.instance.GetWeapon(planetWeapon.data);
        UpdateHand();
    }

    public void GetMaterial(PlanetMaterial material)
    { 
        
            
    }
    void DropWeapon()
    {
        var dropWeapon = WeaponFactory.instance.GetPlanetWeapon(Inventory.instance.currentSlot.currentWeapon);
        if (dropWeapon == null) return;
        int tempDurability = dropWeapon.data.currentDurability;
        var newWeapon = Instantiate(dropWeapon, this.transform.position, Quaternion.identity);
        newWeapon.rb.AddForce(WeaponInput.vectorToWeapon.normalized,ForceMode2D.Impulse);
        newWeapon.data.currentDurability = tempDurability;
        RemoveWeapon();
    }

    void DetectNearestInteractive()
    {
        Vector2 center = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(center, detectRadius);

        IInteractive nearest = null;
        float minDistance = float.MaxValue;

        foreach (Collider2D collider in colliders)
        {
            if (collider == null) continue;
            if (collider.TryGetComponent<IInteractive>(out IInteractive interactable))
            //返回bool值，如果成功则给赋值interactable并且返回true
            {
                float distance = Vector2.Distance(center, collider.transform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = interactable;
                }
            }
        }

        currentInteractive?.EmergeTip(true);
        if (currentInteractive != nearest &&currentInteractive!=null)
        {
            currentInteractive?.EmergeTip(false);
            nearest?.EmergeTip(true);
        }
        //这块是显示气泡逻辑

        currentInteractive = nearest;
    }
    public void TryInteract()
    {
        currentInteractive?.Interact(this);
    }

    void WearWeapon()
    {
        Inventory.instance.CurrentDurability -= 1;
        if (Inventory.instance.CurrentDurability <= 0)
        {
            RemoveWeapon();
        }
    }
    void RemoveWeapon()
    {
        Inventory.instance.SetWeaponEmpty();
        UpdateHand();
    }

}
