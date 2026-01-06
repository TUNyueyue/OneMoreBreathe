using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandAndLauncher : MonoBehaviour
{
    bool canLand;
    bool isLanded;
    LineRenderer line;
    Vector2 planetP;
    Planet currentPlanet;
    [SerializeField] LayerMask planetLayer;
    [SerializeField] float landRadius;
    //懒得引用飞船了，位置需与飞船一致

    public static event Action<Vector3> OnLandPlanet;
    [SerializeField] PlanetPlayer player;
    [SerializeField] CraftInput input;
    //通知move改变位置
    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        BaseCraft.OnLaunchOut += LaunchOutPlanet;
        BaseCraft.OnEnter += OnEnterTheCraft;
        isLanded = true;
        //初始已登陆
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canLand && !isLanded)
        {
            OnLandPlanet.Invoke(planetP);
            player.transform.position = planetP;
            player.ReadPlanet(currentPlanet);
            player.gameObject.SetActive(true);
            Inventory.instance.gameObject.SetActive(true);
            input.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
            isLanded = true;
        }
    }

    public void Detect(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, dir, 100f, planetLayer);
        if (hit.collider != null)
        {
            planetP = hit.point;
            currentPlanet = hit.collider.GetComponent<Planet>();
        }


        Collider2D[] colls = Physics2D.OverlapCircleAll(planetP, landRadius, ~planetLayer);//~表示取反
        canLand = true;
        foreach (Collider2D coll in colls)
        {
            if (coll != null && coll.gameObject.layer != LayerMask.NameToLayer("Craft"))
                canLand = false;
        }
        //Debug.Log(canLand);
    }

    public void Display()
    {
        if (canLand) line.endColor = Color.green;
        else line.endColor = Color.red;
        line.SetPosition(0, this.transform.position);
        line.SetPosition(1, planetP);
    }

    private void OnDrawGizmos()
    {
        if (planetP == null) return;
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(planetP, landRadius);
    }

    void LaunchOutPlanet()
    {
        player.gameObject.SetActive(false);
        //这里其实不用写，保险起见
        Inventory.instance.gameObject.SetActive(false);
        input.gameObject.SetActive(true);
        this.gameObject.SetActive(true);
        isLanded = false;
    }

    void OnEnterTheCraft()
    {
        player.gameObject.SetActive(false);
    }
}
