using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifespanUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI oxygenText;
    [SerializeField] TextMeshProUGUI fuelText;

    void Awake()
    {
        LifespanMgr.instance.OnValueChanged += UpdateUI;
    }
    public void UpdateUI(int healthValue, int oxygenValue, int fuelValue)
    {
        healthText.text = $"Health:{healthValue}";
        oxygenText.text = $"Oxygen:{oxygenValue}";
        fuelText.text = $"Fuel:{fuelValue}";
    }
}
