using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine<T>
{
    public IState CurrentState { get; private set; }
    protected T t;
    public StateMachine(T t)
    {
        this.t = t;
    }
    public void Initialize(IState startState)
    {
        CurrentState = startState;
        startState.Enter();
    }

    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();

    }

    public void Execute()
    {
        if (CurrentState != null)
        {
            CurrentState.Execute();
        }
    }
}
