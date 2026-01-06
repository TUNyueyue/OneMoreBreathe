using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ConsumeButton : MonoBehaviour
{
    Button button;
    [SerializeField] TipsText tip;
    [SerializeField] string craftSuccessTip;
    [SerializeField] int[] craftConsume =new int[4];
    [SerializeField]UnityEvent OnCraft;
    //[SerializeField] bool isConsume = false;
    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TryCraft);
    }

    void TryCraft()
    {
        if (CheckCraftable())
        {
            Craft();
            tip.SetText(craftSuccessTip);
        }
        else         
        {
            tip.SetText("你的材料不够");
        }
    
    }

    bool CheckCraftable()
    {       
        return Warehouse.instance.HasMaterialsOf(craftConsume[0], craftConsume[1],craftConsume[2], craftConsume[3]);
    }

    void Craft()
    {
        Warehouse.instance.ConsumeMaterials(craftConsume[0], craftConsume[1], craftConsume[2], craftConsume[3]);
        //先消耗材料
        OnCraft.Invoke();
        //外包给unity
    }
}
