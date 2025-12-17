using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCraft : SpaceItem, IControllable, ICamerable
{
    [SerializeField] float roationVelocity = 3f;
    [SerializeField] float advancePower = 3f;

    public event Action<Camera> onPull;//注意这是在pull相机
    [SerializeField] Digger digger;
    [SerializeField] SpaceBeing spaceBeing;
    //建立飞船——>人的单向通讯

    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        onPull = Pull;
    }

    public void OnInto()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        SetCollider(true);
    }
    public void OnLeft()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;
        SetCollider(false);
        //取消物理

        spaceBeing.GetCraftTransform(this.transform);
        //把位置信息传给being
    }
    void SetCollider(bool isEnabled)
    {
        Collider2D collider = this.gameObject.GetComponent<Collider2D>();
        collider.enabled = isEnabled;
    }
    void FixedUpdate()
    {

    }
    public void OnGetHorizontal(float Xinput)
    {

        if (Xinput == 1)
        { rb.angularVelocity -= roationVelocity / 2; }
        else if (Xinput == -1)
        { rb.angularVelocity += roationVelocity / 2; }
        else
        {
            if (rb.angularVelocity > 0)
                rb.angularVelocity -= roationVelocity;
            else if (rb.angularVelocity < 0)
                rb.angularVelocity += roationVelocity;
        }
    }

    public void OnGetKeySpace()
    {
        digger.Dig();
    }
    public void OnGetKeyW()
    {
        rb.AddForce(this.transform.up * advancePower);
    }
    public override void OnAttract(Attractor attractor)
    {
        base.OnAttract(attractor);
    }

    public override void OutAttract()
    {
        base.OutAttract();
    }

    public void Pull(Camera camera)
    {
        camera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10f);
        camera.gameObject.transform.rotation = Quaternion.identity;
        camera.orthographicSize = 10f;
    }

    public void OnPull_Invoke(Camera camera)
    {
        onPull.Invoke(camera);
    }

    public bool CheckSwitchable()
    {
        return true;
    }
}
