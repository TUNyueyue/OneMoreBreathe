using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetEnemy : PlanetCharacter, IAttackable
{
    EnemyStateMachine stateMachine;
    [Header("Patrol")]
    [SerializeField] LayerMask wallLayer;
    [SerializeField] Transform wallCheckTrans;
    [SerializeField] float wallCheckLength;
    bool isFaceToWall;
    [Header("Chase")]
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Transform playerCheckTrans;
    [SerializeField] float playerCheckLength;
    [SerializeField] float detectAngle = 45f;
    float distanceToPlayer = 100f;
    bool isFaceToPlayer;
    [Header("Attack")]
    [SerializeField] float attackDistance = 3f;
    [SerializeField] DropItem dropItem;
    [SerializeField] Weapon weapon;
    [SerializeField] WeaponData dropWeapon;
    [field: SerializeField] public int Health { get; set; }
    public event Action OnDeath;
    public AttackedType type { get; set; }

    protected override void Awake()
    {
        base.Awake();
        type = AttackedType.Character;
        stateMachine = new EnemyStateMachine(this);
        stateMachine.Initialize(stateMachine.patrolState);
    }

    void Start()
    {

    }

    void OnEnable()
    {
        OnDeath += OnEnemyDeath;
    }
    void OnDisable()
    {
        OnDeath -= OnEnemyDeath;
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.Execute();
        isFaceToWall = Physics2D.Raycast(wallCheckTrans.position, this.transform.right * faceDir, wallCheckLength, wallLayer);
        //isFaceToPlayer = Physics2D.Raycast(playerCheckTrans.position, this.transform.right * faceDir, playerCheckLength, playerLayer);
        // 扇形检测
        isFaceToPlayer = false;
        float radius = playerCheckLength;  // 扇形半径
        // 1. 先圆形检测范围内所有对象
        Collider2D[] hits = Physics2D.OverlapCircleAll(playerCheckTrans.position, radius, playerLayer);

        foreach (Collider2D hit in hits)
        {
            distanceToPlayer = (hit.transform.position - playerCheckTrans.position).magnitude;
            Vector2 directionToTarget = (hit.transform.position - playerCheckTrans.position).normalized;
            float angleToTarget = Vector2.Angle(this.transform.right * faceDir, directionToTarget);

            // 2. 判断是否在扇形角度内
            if (angleToTarget < detectAngle / 2f)
            {
                isFaceToPlayer = true;
                break;
            }
        }
        //if (isFaceToPlayer) Debug.Log("检测到玩家");
    }
    void OnDrawGizmos()
    {
        Vector3 wallEnd = wallCheckTrans.position + this.transform.right * faceDir * wallCheckLength;
        Gizmos.DrawLine(wallCheckTrans.position, wallEnd);
        //Vector3 playerEnd = playerCheckTrans.position + this.transform.right * faceDir * playerCheckLength;
        //Gizmos.DrawLine(playerCheckTrans.position, playerEnd);

        // 扇形参数
        float radius = playerCheckLength;
        float halfAngle = detectAngle / 2;

        // 绘制扇形
        Vector3 forward = this.transform.right * faceDir;

        // 扇形边线
        Vector3 startDir = Quaternion.Euler(0, 0, -halfAngle) * forward;
        Vector3 endDir = Quaternion.Euler(0, 0, halfAngle) * forward;

        Gizmos.color = isFaceToPlayer ? Color.green : Color.red;
        Gizmos.DrawLine(playerCheckTrans.position, playerCheckTrans.position + startDir * radius);
        Gizmos.DrawLine(playerCheckTrans.position, playerCheckTrans.position + endDir * radius);

        // 扇形弧线
        Vector3 prev = playerCheckTrans.position + startDir * radius;
        for (int i = 1; i <= 8; i++)
        {
            float angle = -halfAngle + (i * 2 * halfAngle / 8);
            Vector3 dir = Quaternion.Euler(0, 0, angle) * forward;
            Vector3 curr = playerCheckTrans.position + dir * radius;
            Gizmos.DrawLine(prev, curr);
            prev = curr;
        }
    }

    public void FlipController()
    {
        if (isFaceToWall) Flip();

    }
    public void PatrolMove()
    {
        Move(faceDir);
    }

    public void ChaseMove()
    {
        Move(faceDir);
    }

    public void StopMoving()
    {
        rb.velocity = Vector3.zero;
    }

    public bool PatrolToChase()
    {
        return isFaceToPlayer;
    }
    public bool ChaseToPatrol()
    {
        return !isFaceToPlayer;
    }
    public bool ChaseToAttack()
    {
        return distanceToPlayer < attackDistance;
    }

    public void OnAttack(int damage)
    {
        
        Health -= damage;
        if (Health <= 0)
            OnDeath.Invoke();
    }

    void OnEnemyDeath()
    {
        for (int i = 0; i < dropItem.num; i++)
        { Instantiate(dropItem.objectToDrop, this.transform.position, Quaternion.identity); }
        DropWeapon();
        Destroy(gameObject);
    }

    public void UseWeapon()
    {
        
        weapon.Use(this.transform.right * faceDir);
    }

    public void DropWeapon()
    {
        var dropWeapon = WeaponFactory.instance.GetPlanetWeapon(this.dropWeapon);
        if (dropWeapon == null) return;
        int tempDurability = dropWeapon.data.currentDurability;
        var newWeapon = Instantiate(dropWeapon, this.transform.position, Quaternion.identity);
        newWeapon.rb.AddForce(-WeaponInput.vectorToWeapon.normalized, ForceMode2D.Impulse);
    }

    public void StartEnemyCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}
