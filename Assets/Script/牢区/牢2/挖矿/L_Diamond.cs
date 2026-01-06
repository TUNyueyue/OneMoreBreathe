using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_Diamond : MonoBehaviour,L_IPickable
{
    [SerializeField] L_LevelData levelData;
    Collider2D _collider;
    public void OnPickUp(L_PlanetBeing player)
    {
        levelData.diamondNum++;
        transform.SetParent(player.transform);
    }

    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

   
    void Update()
    {
        
    }
}
