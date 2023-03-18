using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine; //protected - only classes that inherit this can access the state machine

    public PlayerBaseState(PlayerStateMachine stateMachine) // A constructor is a function thats called when you create a new instance of the class
    {
       this.stateMachine = stateMachine;
    }
}
