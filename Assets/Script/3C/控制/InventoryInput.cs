using System;
using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    [SerializeField] float scrollCooldown = 0.2f;
    float lastScrollTime = 0f;

    public static event Action<int> OnChangeSlot;
    public static event Action OnGetKeyDownQ;
    

    void Update()
    {
        CheckKeyDown();



        //先检测逻辑放上面
        bool flowControl = CheckScroll();
        if (!flowControl)
        {
            return;
        }
        //这是系统自动提取的果然有坑，这个检测只能放最下面

    }

    void CheckKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnGetKeyDownQ.Invoke();
        }
    }

    bool CheckScroll()
    {
        if (Time.time - lastScrollTime < scrollCooldown) return false;

        float scrollValue = Input.GetAxis("Mouse ScrollWheel");

        if (scrollValue != 0f)
        {
            if (scrollValue > 0f)
            {
                OnChangeSlot.Invoke(1);
                //Debug.Log("向上滚动");
            }
            else
            {
                OnChangeSlot.Invoke(-1);
                //Debug.Log("向下滚动");
            }

            lastScrollTime = Time.time;
        }

        return true;
    }
}