using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//猜你在找：查找快捷键ctrl+F

public class L_PlanetBeing : L_PlanetItem, L_IControllable, L_ICamerable
{
    Transform attractorTrans;
    //这个东西有点危险说实话
    [SerializeField] Transform craftTrans;
    //飞船信息,当脱离飞船时获取,直接拖也没问题说实话XD
    Vector2 vecToAttractor = new Vector2(0, -30);
    //指向行星,给个初始值不然报错,草不是这个问题，哪来的鼠标检测
    //应该是distance为0触发的问题
    [SerializeField] L_Rope rope;
    [SerializeField] float intoDistance;
    //可进入飞船距离
    [SerializeField] float tiedDistance;
    //绳子可拉回距离
    bool isTied = false;
    //初始无绳索

    [SerializeField] float advanceForce = 10f;
    [SerializeField] float roationSpeed = 10f;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float pullVelocity = 1f;
    [SerializeField] float impulseScale = 0.3f;

    enum MoveState { inPlanet, outPlanet };
    MoveState state;
    //依旧if地狱

    public event Action<Camera> onPull;
    //注意这是在pull相机

  
    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        onPull = Pull_In;
        state = MoveState.inPlanet;//以防万一
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        var pickableItem = collider.gameObject.GetComponent<L_IPickable>();
        if (pickableItem != null)
        {
            pickableItem.OnPickUp(this);       
        }
    }
    //简单写写
    public void OnInto()
    {
        this.gameObject.SetActive(true);
        if (craftTrans != null)
            this.transform.position = craftTrans.position;
        isTied = true;
        this.transform.up=craftTrans.up;
        rb.AddForce(this.craftTrans.up * advanceForce * impulseScale, ForceMode2D.Impulse);
        //弹射起步！！！！
    }
    //这里的Into不是进入飞船而是从飞船切换到这个控制器

    public void OnLeft()
    {
        StartCoroutine(PulledBackToCraft());
    }
    public float ReturnPullBackDuration()
    {
        Vector3 startP = this.transform.position;
        Vector3 endP = craftTrans.position;
        return Vector2.Distance(startP, endP) / pullVelocity;
    }
    //添加绳子拉回协程
    IEnumerator PulledBackToCraft()
    {
        float time = 0f;
        float duration = ReturnPullBackDuration();
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            rb.position = Vector3.Lerp(this.transform.position, craftTrans.position, t);
            yield return null;
        }
        Debug.Log("PullOver！");
        isTied = false;
        rope.SetTied(isTied);
        RemovePickableFromChildren();
        //回到飞船，清除捡到的东西
        this.gameObject.SetActive(false);
    }
    void RemovePickableFromChildren()
    {
        foreach (Transform child in transform)
        {
            var pickable = child.GetComponent<L_IPickable>();
            if (pickable != null)
            {
                Destroy(child.gameObject);
            }
        }
    }
    void FixedUpdate()
    {

    }

    void Update()
    {
        if (attractorTrans != null)
        {
            vecToAttractor = attractorTrans.position - this.transform.position;
        }



        //绳子是否断开检测
        if (isTied)
        {
            rope.display(craftTrans.position, this.transform.position);
            if (Vector2.Distance(craftTrans.position, this.transform.position) > tiedDistance)
            {
                isTied = false;
                Debug.Log("绳子断了！");
            }
            rope.SetTied(isTied);//这里有优化空间，但我懒
        }
    }
    public void OnGetHorizontal(float Xinput)
    {
        if (state == MoveState.inPlanet)
        {
            this.transform.up = -vecToAttractor;
            //脚朝地

            Vector2 tangentDir = new Vector2(-vecToAttractor.y, vecToAttractor.x).normalized;
            float currentSpeed = Vector2.Dot(rb.velocity, tangentDir);
            //点积单位向量算投影，即当前切向速度
            float targetSpeed = Xinput * moveSpeed;

            float newSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, 10f * Time.fixedDeltaTime);
            float speedChange = newSpeed - currentSpeed;
            Vector2 velocityChange = tangentDir * speedChange;
            rb.velocity += velocityChange;
        }
        if (state == MoveState.outPlanet)
        {

            if (Xinput == 1)
            { rb.angularVelocity = -roationSpeed * 10f; }
            else if (Xinput == -1)
            { rb.angularVelocity = roationSpeed * 10f; }
            else { rb.angularVelocity = 0f; }

        }
    }
    public void OnGetKeySpace()
    {
      
    }
    public void OnGetKeyDownSpace()
    {
        if (state == MoveState.inPlanet)
        { rb.AddForce(this.transform.up * advanceForce *20f); }
        if (state == MoveState.outPlanet)
        { 
            //rb.AddForce(this.transform.up * advanceForce * impulseScale, ForceMode2D.Impulse); 
        }
    }
    public void OnGetKeyW()
    {
        if (state == MoveState.inPlanet)
        { }
        if (state == MoveState.outPlanet)
        {
            rb.AddForce(this.transform.up * advanceForce / 2);
        }
    }
    public override void OnAttract(L_Attractor attractor)
    {
        base.OnAttract(attractor);
        attractorTrans = attractor.transform;
        onPull = Pull_In;
        state = MoveState.inPlanet;
    }

    public override void OutAttract()
    {
        base.OutAttract();
        onPull = PullOut;
        state = MoveState.outPlanet;
    }

    public void Pull_In(Camera camera)
    {
        camera.gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10f);
        camera.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, -this.vecToAttractor.normalized);
        float distance = vecToAttractor.magnitude;
        camera.orthographicSize = 5f * (distance + 1f) / 10f;
    }
    public void PullOut(Camera camera)
    {
        camera.gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10f);
        camera.gameObject.transform.rotation = Quaternion.identity;
        float distance = Vector2.Distance(this.transform.position, craftTrans.position);
        camera.orthographicSize = 5f * (distance+3f) / 5f;//防止镜头太近，+5f接近飞船镜头
        if (camera.orthographicSize > 10f)
            camera.orthographicSize = 10f;//防止镜头太远
    }
    //上面俩接口方法

    public void OnPull_Invoke(Camera camera)
    {
        onPull.Invoke(camera);
    }
    public void GetCraftTransform(Transform craftTrans)
    {
        this.craftTrans = craftTrans;
    }
    public Transform ReturnCraftTransform()
    {
        return this.craftTrans;
    }
    public bool CheckSwitchable()
    {
        if (HasRopeTied()) return true;
        else return BaseDistanceCheck();
    }
    bool BaseDistanceCheck()
    {
        if (Vector2.Distance(craftTrans.position, this.transform.position) <= intoDistance)
        {
            Debug.Log("正常进入");
            return true;
        }
        else return false;
    }
    //当飞船距离人太远时，不能进入

    bool HasRopeTied()
    {
        return isTied;
    }
    //检查人物是否被绳索栓着

}
