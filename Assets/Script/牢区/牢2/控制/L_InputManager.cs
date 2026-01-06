using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class L_InputManager : MonoBehaviour
{
    L_IControllable controller;
    [SerializeField] L_SpaceCraft spaceCraft;
    [SerializeField] L_PlanetBeing spaceBeing;
    [SerializeField] L_SpaceCamera spaceCamera;
    

    float Xinput;



    [SerializeField]L_LevelData levelData;
    void Start()
    {
        controller = spaceBeing;
        spaceCamera.subject = spaceBeing;

    }


    void Update()
    {
        Xinput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {

            controller.OnGetKeyDownSpace();
            Debug.Log("Jump!");
        }

        else if (Input.GetKey(KeyCode.Space))
        {
            controller.OnGetKeySpace();
            levelData.fuelValue -= 0.1f;
            //暂时先放这
        }
 
        //双space检测可能会出现问题


        if (Input.GetKeyDown(KeyCode.E) && controller.CheckSwitchable())
        {
            controller.OnLeft();
            if (Equals(controller, spaceCraft))
            {
                controller = spaceBeing;
                spaceCamera.subject = spaceBeing;
                controller.OnInto();
            }
            else
            {
                StartCoroutine(WaitToCraft(spaceBeing.ReturnPullBackDuration()));
            }
            
        }
        //这部分记得拆开,哎呦这怎么还有相机的逻辑啊难分啊
    }
    IEnumerator WaitToCraft(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        spaceCamera.subject = spaceCraft;
        controller = spaceCraft;
        controller.OnInto();
    }
    //把拉绳子的协程带过来
    void FixedUpdate()
    {
        controller.OnGetHorizontal(Xinput);

        if (Input.GetKey(KeyCode.W))
        {
            controller.OnGetKeyW();
        }
        if (Input.GetKey(KeyCode.S))
        {
            controller.OnGetKeyS();
        }

    }

    IEnumerator LandOnPlanet()
    {
        yield return new WaitForSeconds(5f);
    }//登陆协程，主要处理舱门开关，镜头平滑过度，控制对象切换
}
