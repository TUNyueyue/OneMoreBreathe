using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Warehouse : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] WarehouseSO warehouseSO;
    [Header("UI")]
    [SerializeField] TextMeshProUGUI[] quantityTexts;

    public static Warehouse instance;

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

    public void UpdateUI()
    {
        for (int i = 0; i < quantityTexts.Length; i++)
        {
            quantityTexts[i].text = warehouseSO.GetQuantityByName(quantityTexts[i].gameObject.name).ToString();
        }
    }

    public void AddMaterialByName(string name, int num = 1)
    {
        warehouseSO.AddMaterialByName(name, num);
        UpdateUI();
    }

    public void AddCurrency(int num)
    {
        warehouseSO.AddMaterialByName("Currency", num);
        UpdateUI();
    }

    public bool HasMaterialsOf(int bNum = 0, int gNum = 0, int cNum = 0, int CNum = 0)
    {
        
        if (warehouseSO.GetQuantityByName("Branch") >= bNum
            && warehouseSO.GetQuantityByName("Gear") >= gNum
            && warehouseSO.GetQuantityByName("Crystal") >= cNum
            && warehouseSO.GetQuantityByName("Currency") >= CNum
            )
        {

            return true;

        }
        return false;
    }

    public void ConsumeMaterials(int bNum = 0, int gNum = 0, int cNum = 0,int CNum = 0)
    {
        warehouseSO.RemoveMaterialByName("Branch", bNum);
        warehouseSO.RemoveMaterialByName("Gear", gNum);
        warehouseSO.RemoveMaterialByName("Crystal", cNum);
        warehouseSO.RemoveMaterialByName("Currency", CNum);
        UpdateUI();
    }

}
