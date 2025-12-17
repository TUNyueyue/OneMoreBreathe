using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]LevelData levelData;
    [SerializeField] TMP_Text fuelText;
    [SerializeField] TMP_Text diamondText;
    void Start()
    {
        
    }

    
    void Update()
    {
        fuelText.text = $"FuelValue:{levelData.fuelValue:0}";
        diamondText.text = $"Diamond:{levelData.diamondNum}";
    }
}
