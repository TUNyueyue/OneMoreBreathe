using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseCraft : MonoBehaviour,IInteractive
{
    [Header("PlanetDetect")]
    [SerializeField] float enterRadius = 3f;
    [SerializeField] float exitRadius = 5f;
    [SerializeField] LayerMask targetLayer;
    bool isTargetInRange = false;
    [Header("PlanetInfo")]
    Transform planetTrans;
    public Vector2 vecToPlanet;
    [Header("LandOnPlanet")]
    [SerializeField] LandAndLauncher landDetecter;

    [Header("UI")]
    [SerializeField] GameObject craftPanel; 
    [field: SerializeField] public GameObject Tip { get; set; }


    Rigidbody2D rb;

    public static event Action OnLaunchOut;
    public static event Action OnEnter;

    //public static event Action<Transform> OnEnterPlanet;
    //进入星球事件，传入星球位置
    //public static event Action OnExitPlanet;
    //事件有问题啊老报错

    void Awake()
    {
        craftPanel.SetActive(false);
        Tip.SetActive(false);
    }
    void Start()
    {
        landDetecter.gameObject.SetActive(false);
        rb = GetComponent<Rigidbody2D>();   
    }

    void OnEnable()
    {
        LandAndLauncher.OnLandPlanet += LandOnPlanet;
    }

    void OnDisable()
    {
        LandAndLauncher.OnLandPlanet -= LandOnPlanet;
    }
    void Update()
    {
        
        Collider2D target = Physics2D.OverlapCircle(transform.position,
            isTargetInRange ? exitRadius : enterRadius, targetLayer);
        if (target != null && !isTargetInRange)
        {
            OnEnterRange(target);
        }
        else if (target == null && isTargetInRange)
        {
            OnExitRange();
        }
        FootToPlanet();
        //这个一定要放在获取planetTrans下面，不然会跳过影响Detect(vecToPlanet)

    }
    void LateUpdate()
    {
        if (isTargetInRange)
        {
            landDetecter.Detect(vecToPlanet);
            landDetecter.Display();
        }
    }

    void OnEnterRange(Collider2D planet)
    {
        isTargetInRange = true;
        planetTrans = planet.gameObject.transform;
        landDetecter.gameObject.SetActive(true);
        //Debug.Log("目标进入范围 - 触发一次");
    }

    void OnExitRange()
    {
        isTargetInRange = false;
        planetTrans = null;
        transform.rotation = Quaternion.identity;
        landDetecter.gameObject.SetActive(false);
        //Debug.Log("目标退出范围 - 触发一次");
    }

    // 调试
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, enterRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, exitRadius);
    }
    void FootToPlanet()
    {
        if (planetTrans == null) return;
        vecToPlanet = planetTrans.position - this.transform.position;
        Vector3 direction = -vecToPlanet;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    void LandOnPlanet(Vector3 pos)
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        Collider2D collider = GetComponent<Collider2D>();
        collider.isTrigger = true;

    }

    public void LaunchOutPlanet()
    {
        OnLaunchOut.Invoke();
        rb.bodyType = RigidbodyType2D.Dynamic;
        Collider2D collider = GetComponent<Collider2D>();
        collider.isTrigger = false;
        Tip.SetActive(false);
    }

    public void Interact(PlayerHand hand)
    {
        Tip.SetActive(false);
        craftPanel.SetActive(true);
        OnEnter.Invoke();
    }

    //IEnumerator LaterExecute()
    //{
    //    yield return new WaitForEndOfFrame();
    //    Collider2D collider = GetComponent<Collider2D>();
    //    collider.isTrigger = true;
    //}
    ////笑死，延迟一帧也没用
}
