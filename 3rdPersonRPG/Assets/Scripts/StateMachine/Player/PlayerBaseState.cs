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

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.characterController.Move((motion + stateMachine.ForceReceiver.movement) * deltaTime);
    }

    protected void FaceTarget() {

        if (stateMachine.Targeter.currentTarget == null) { return; } //do not run

        Vector3 lookPosition = stateMachine.Targeter.currentTarget.transform.position - stateMachine.transform.position; //The vector pointing from player to target 
        lookPosition.y = 0;

        //Convert to Quaternion
        stateMachine.transform.rotation = Quaternion.LookRotation(lookPosition);
    }
}
