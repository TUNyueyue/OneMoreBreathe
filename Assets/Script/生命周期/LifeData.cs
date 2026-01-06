using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeData : MonoBehaviour
{
    public int healthValue { get; private set; }
    public int oxygenValue { get; private set; }
    public int fuelValue { get; private set; }
    [SerializeField] int maxHealth;
    [SerializeField] int maxOxygen;
    [SerializeField] int maxFuel;
    public void SetHealth(int value) => healthValue = Mathf.Clamp(value, 0, maxHealth);
    public void SetOxygen(int value) => oxygenValue = Mathf.Clamp(value, 0, maxOxygen);
    public void SetFuel(int value) => fuelValue = Mathf.Clamp(value, 0, maxFuel);

    public void ResetVaule()
    {
        healthValue = maxHealth;
        oxygenValue = maxOxygen;
        fuelValue = maxFuel;
    }

    public void ResetHealth()
    { healthValue = maxHealth; }

    public void ResetOxygen()
    { oxygenValue = maxOxygen; }

    public void ResetFuel()
    { fuelValue = maxFuel; }

}
