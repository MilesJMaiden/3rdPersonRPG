using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine; //protected - only classes that inherit this can access the state machine

    public EnemyBaseState(EnemyStateMachine stateMachine) // A constructor is a function thats called when you create a new instance of the class
    {
        this.stateMachine = stateMachine;
    }
}
