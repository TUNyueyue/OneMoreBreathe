using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{

    EnemyStateMachine stateMachine;
    PlanetEnemy enemy;
    public AttackState(PlanetEnemy enemy, EnemyStateMachine stateMachine)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
    }
    public void Enter()
    {
        enemy.StartEnemyCoroutine(AttackTime());
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {

    }

    IEnumerator AttackTime()
    {
        enemy.StopMoving();
        enemy.UseWeapon();
        yield return new WaitForSeconds(1f);
        stateMachine.TransitionTo(stateMachine.patrolState);
    }

}
