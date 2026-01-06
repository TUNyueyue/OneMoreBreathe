using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{
    EnemyStateMachine stateMachine;
    PlanetEnemy enemy;
    public ChaseState(PlanetEnemy enemy, EnemyStateMachine stateMachine)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
    }
    public void Enter()
    {

    }

    public void Execute()
    {
        enemy.ChaseMove();

        if (enemy.ChaseToPatrol())
        {
            stateMachine.TransitionTo(stateMachine.patrolState);
        }
        if (enemy.ChaseToAttack())
        {
            stateMachine.TransitionTo(stateMachine.attackState);
        }
    }


    public void Exit()
    {

    }
}
