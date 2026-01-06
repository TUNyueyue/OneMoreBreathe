using System;
using UnityEngine;

public class WeaponInput : MonoBehaviour
{
    Vector2 mouseWorldPos;
    [SerializeField] Transform handTrans;
    public static Vector2 vectorToWeapon { get; private set; }
    //¸Ð¾õºÃÎ£ÏÕ°¡
    public static event Action OnClickMouse;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClickMouse.Invoke();
        }

        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 vectorToMouse = new Vector2(mouseWorldPos.x - handTrans.position.x, mouseWorldPos.y - handTrans.position.y);
        vectorToWeapon = vectorToMouse.normalized * 0.2f;
    }

    //µ÷ÊÔ
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(handTrans.position, mouseWorldPos);
    }
}
