using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    EnemyStateMachine stateMachine;
    PlanetEnemy enemy;
    public PatrolState(PlanetEnemy enemy,EnemyStateMachine stateMachine)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {
        
    }

    public void Execute()
    {
        enemy.PatrolMove();
        enemy.FlipController();
        if (enemy.PatrolToChase())
        {
            stateMachine.TransitionTo(stateMachine.chaseState);
        }
    }

    public void Exit()
    {
       
    }
}
