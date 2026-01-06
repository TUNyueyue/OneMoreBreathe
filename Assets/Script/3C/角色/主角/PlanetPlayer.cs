using System;
using System.Collections.Generic;
using UnityEngine;

public class PlanetPlayer : PlanetCharacter, IAttackable
{
    [SerializeField] float breatheRate = 1f;
    float onceOxygenConsume = 0;
    bool hasBreath = true;
    public AttackedType type { get; set; }

    protected override void Awake()
    {
        base.Awake();
        type = AttackedType.Character;
    }

    void OnEnable()
    {
        PlayerHand.OnUseWeapon += AddRecoil;
    }
    void OnDisable()
    {
        PlayerHand.OnUseWeapon -= AddRecoil;
    }
    void Start()
    {

    }


    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        float XInput = Input.GetAxis("Horizontal");
        Move(XInput);
        FilpController();
        if(!hasBreath)
        ConsumeOxygen();
    }

    void FilpController()
    {
        Vector2 mouseP = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if (mouseP.x > 0.5f && !faceright)
            Flip();
        else if (mouseP.x < 0.5f && faceright)
            Flip();
    }
    void AddRecoil()//Recoil后座力
    {
        Vector2 tangentDir = new Vector2(-vecToPlanet.y, vecToPlanet.x).normalized;
        //方向向右（玩家坐标系）
        rb.AddForce(tangentDir * faceDir * 100f * -1f);
    }

    public void ReadPlanet(Planet planet)
    {
        if(PlanetTrans != null)
        PlanetTrans.GetComponent<Planet>().RemoveEntityInForce(this);
        PlanetTrans = planet.transform;
        hasBreath = planet.hasBreath;

    }
    public void OnAttack(int damage)
    {
        LifespanMgr.instance.ChangeHealth(-damage);
    }

    public void ConsumeOxygen()
    {
        onceOxygenConsume += Time.deltaTime * breatheRate;
        if (onceOxygenConsume >= 1)
        {
            onceOxygenConsume -= 1;
            LifespanMgr.instance.ChangeOxygen(-1);
        }

    }
}
