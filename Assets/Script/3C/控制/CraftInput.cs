using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftInput : MonoBehaviour
{
    public static event Action<float, float> OnCraftMove;

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        OnCraftMove.Invoke(moveX, moveY);

    }
}
