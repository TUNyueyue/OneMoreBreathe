using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_Rope : MonoBehaviour
{
    //暂不实现物理效果
    LineRenderer line;
    Vector2 startP;
    Vector2 endP;



    void OnEnable()
    {

    }
    void OnDisable()
    {

    }
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
    }
    void Update()
    {
        //Tie(spaceBeing.ReturnCraftTransform().position,spaceBeing.transform.position);
    }

    public void display(Vector2 startP, Vector2 endP)
    {
        if (line.positionCount == 0) return;
        line.SetPosition(0, startP);
        line.SetPosition(1, endP);
    }
    public void SetTied(bool isTied)
    {
        line.positionCount = 2;
        if (!isTied)
            line.positionCount = 0;
        this.gameObject.SetActive(isTied);
    }

}
