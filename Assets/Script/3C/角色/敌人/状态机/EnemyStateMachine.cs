using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine<PlanetEnemy>
{
    public PatrolState patrolState { get; private set; }
    public ChaseState chaseState { get; private set; }
    public AttackState attackState { get; private set; }
    //只允许状态机管理状态，所以这里是private set
    public EnemyStateMachine(PlanetEnemy planetEnemy) : base(planetEnemy)
    {
        patrolState = new PatrolState(planetEnemy,this);
        chaseState = new ChaseState(planetEnemy,this);
        attackState = new AttackState(planetEnemy,this);
    }

}
