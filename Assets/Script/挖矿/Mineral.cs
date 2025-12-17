using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral : MonoBehaviour,IMinable
{
    [SerializeField]int durability;
    [SerializeField] GameObject dropItem;
    public int Durability
    {
        get => durability;
        set => durability = Mathf.Max(0, value);
    }

    public void Consume(int value)
    {
        Durability -= value;
        OnBreak();
        Debug.Log("beingMining");
    }

    public void OnBreak()
    {
    if(Durability<=0)
        {
            Instantiate(dropItem,this.transform.position,Quaternion.identity);
            Destroy(this.gameObject);
        }
    }//ÏÈ¼òµ¥Ð´Ð´

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
