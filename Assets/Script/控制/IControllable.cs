using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllable
{
    void OnInto();
    void OnLeft();

    bool CheckSwitchable();
    void OnGetKeySpace()
    { }
    void OnGetKeyDownSpace()
    { }
    //怎么还有默认实现的啊，我是懒狗
    void OnGetKeyW()
    { }
    void OnGetKeyS()
    { }
    void OnGetHorizontal(float Xinput);

  
    

}
