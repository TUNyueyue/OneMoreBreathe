using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftMove : MonoBehaviour
{
    [SerializeField]float moveSpeed = 5f;
    [SerializeField] float landOffset = 1f;
    Rigidbody2D rb;
    //最简单的Translate移动
    float onceFuelConsume = 0;
    [SerializeField] float consumeRate =1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnEnable()
    {
        LandAndLauncher.OnLandPlanet += LandOnPlanet;
        CraftInput.OnCraftMove += Move;
    }

    void OnDisable()
    {
        LandAndLauncher.OnLandPlanet -= LandOnPlanet;
        CraftInput.OnCraftMove -= Move;
    }
    void Update()
    {
        
    }

    void Move(float moveX,float moveY)
    {
        Vector3 move = new Vector3(moveX, moveY, 0) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);
        //Space.World,相对世界坐标系移动
        float consume = move.magnitude * consumeRate;
        //move.magnitude相当于一帧的路程
        onceFuelConsume += consume;
        if (onceFuelConsume >= 1)
        {
            LifespanMgr.instance.ChangeFuel(-1);
            onceFuelConsume -= 1;
        }
        //消耗燃料
    }

    void LandOnPlanet(Vector3 pos)
    {
        transform.position = pos + transform.up * landOffset;
        this.rb.velocity = Vector3.zero;
    }
}
