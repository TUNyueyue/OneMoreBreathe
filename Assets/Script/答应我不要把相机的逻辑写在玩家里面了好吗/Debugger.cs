using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField] WarehouseSO warehouseSO;
    void Start()
    {
        warehouseSO.Reset();
    }

    void Update()
    {
        
    }
}
