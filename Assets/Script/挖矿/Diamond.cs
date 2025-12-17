using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour,IPickable
{
    [SerializeField] LevelData levelData;
    Collider2D _collider;
    public void OnPickUp(SpaceBeing player)
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
