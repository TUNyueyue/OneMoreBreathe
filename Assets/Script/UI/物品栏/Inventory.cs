using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Inventory : MonoBehaviour//注意是不销毁单例
{//武器数据跟你有啥关系啊，你只负责处理slot
    public static Inventory instance;
    public Slot currentSlot { get; private set; }
    public int CurrentDurability
    {
        get => this.currentSlot.currentWeapon.currentDurability;
        set => this.currentSlot.currentWeapon.currentDurability = value;
    }//省点代码
    int slotIndex;
    [SerializeField] int maxCapacity = 3;
    [SerializeField] List<Slot> slots;

    public static event Action OnSelectEvent;//由于多播委托没有顺序所以新加了个事件
    //这里如果用单例调用的话会触发闭包


    ////------------
    //[SerializeField] Materials materials;
    //public static event Action OnDropMaterials;
    ////物品栏直接对接材料，因为材料是一次性的，不需要具体到某个slot
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
        //单例逻辑，跨场景不销毁

        UpdateMaxCapacity();
        currentSlot = slots[0];
    }
    void Start()
    {      
        currentSlot.OnSelect();
    }
    void OnEnable()
    {
        InventoryInput.OnChangeSlot += OnSelect;
    }

    void OnDisable()
    {
        InventoryInput.OnChangeSlot -= OnSelect;
    }
    void UpdateMaxCapacity()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < maxCapacity; i++)
        {
            slots[i].gameObject.SetActive(true);
        }
    }
    void OnSelect(int scrollDir)//1代表上，-1代表下
    {
        currentSlot.OutSelect();
        if (scrollDir == 1)
        {
            LastSlot();
        }
        else if (scrollDir == -1)
        {
            NextSlot();
        }
        currentSlot.OnSelect();
        OnSelectEvent?.Invoke();
    }

    void NextSlot()
    {
        currentSlot = slots[(slotIndex + 1) % maxCapacity];
        slotIndex++;
        slotIndex = slotIndex % maxCapacity;
    }
    void LastSlot()
    {
        if (slotIndex == 0)
            slotIndex = maxCapacity;
        currentSlot = slots[slotIndex - 1];
        slotIndex--;
    }
    void SetMaxCapacity(int maxNum)
    {
        if (maxNum < 1 || maxNum > 5) return;
        maxCapacity = maxNum;
    }
    public void GetWeapon(WeaponData weapon)
    {
        Slot slot = CheckEmptySlot();
        if (slot != null)
        {
            slot.SetWeapon(weapon);
        }
        else
        {
            //这里写物品栏满了的逻辑
        }
    }

    public void SetWeaponEmpty()
    {
        currentSlot.SetEmpty();
    }

    public Slot CheckEmptySlot()
    {
        for (int i = 0; i < maxCapacity; i++)
        {
            if (slots[i].isEmpty == true)
                return slots[i];
        }
        return null;
    }

    public void AddSlot()
    {        
        maxCapacity++;
        UpdateMaxCapacity();
    }
}
