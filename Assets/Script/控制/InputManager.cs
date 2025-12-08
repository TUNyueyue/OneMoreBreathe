using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour
{
    IControllable controller;
    [SerializeField] SpaceCraft spaceCraft;
    [SerializeField] SpaceBeing spaceBeing;
    [SerializeField] SpaceCamera spaceCamera;
    

    float Xinput;
    enum SpaceKeyState { None, Down, Hold };
    SpaceKeyState spaceState;
    void Start()
    {
        controller = spaceBeing;
        spaceCamera.subject = spaceBeing;
        spaceState = SpaceKeyState.None;
    }


    void Update()
    {
        Xinput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceState = SpaceKeyState.Down;
            controller.OnGetKeyDownSpace();
            Debug.Log("Jump!");
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            spaceState = SpaceKeyState.Hold;
        }
        if (Input.GetKey(KeyCode.W))
        {
            controller.OnGetKeyW();
        }

        //双space检测可能会出现问题


        if (Input.GetKeyDown(KeyCode.E) && controller.CheckSwitchable())
        {
            controller.OnLeft();
            if (Equals(controller, spaceCraft))
            {
                controller = spaceBeing;
                spaceCamera.subject = spaceBeing;
            }
            else
            {
                controller = spaceCraft;
                spaceCamera.subject = spaceCraft;
            }
            controller.OnInto();
        }
        //这部分记得拆开
    }

    void FixedUpdate()
    {
        controller.OnGetHorizontal(Xinput);
        if (spaceState == SpaceKeyState.Hold)
            controller.OnGetKeySpace();
        spaceState = SpaceKeyState.None;
    }

    IEnumerator LandOnPlanet()
    {
        yield return new WaitForSeconds(5f);
    }//登陆协程，主要处理舱门开关，镜头平滑过度，控制对象切换
}
