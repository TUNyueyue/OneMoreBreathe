using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifespanMgr : MonoBehaviour
{
    public static LifespanMgr instance;
    public LifeData data { get; private set; }
    public event Action OnLifespanRunOut;
    public event Action<int, int, int> OnValueChanged;
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
    void Start()
    {
        data = GetComponent<LifeData>();
        data.ResetVaule();
        UpdateUI();
    }



    public void ChangeHealth(int value)
    {
        data.SetHealth(data.healthValue + value);
        CheckLifespan();
        UpdateUI();
    }
    public void ChangeOxygen(int value)
    {
        data.SetOxygen(data.oxygenValue + value);
        CheckLifespan();
        UpdateUI();
    }
    public void ChangeFuel(int value)
    {
        data.SetFuel(data.fuelValue + value);
        CheckLifespan();
        UpdateUI();
    }

    public void RecoverHealth()
    {
        data.ResetHealth();
        UpdateUI();
    }
    public void RecoverOxygen()
    {
        data.ResetOxygen();
        UpdateUI();
    }
    public void RecoverFuel()
    {
        data.ResetFuel();
        UpdateUI();
    }
    void CheckLifespan()
    {

        if (data.healthValue <= 0 || data.oxygenValue <= 0 || data.fuelValue <= 0)
        {
            OnLifespanRunOut?.Invoke();
            Debug.Log("游戏结束了！！！");
            return;
        }
    }

    void UpdateUI()
    {
        OnValueChanged.Invoke(data.healthValue, data.oxygenValue, data.fuelValue);
        //通知UI
    }


}
