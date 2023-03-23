using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{

    private Attack attack;
    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackId) : base(stateMachine) //passes in extra parameter 'attackId' into contructor
    {
        attack = stateMachine.Attacks[attackId];
    }

    public override void Enter()
    {
        stateMachine.animator.CrossFadeInFixedTime(attack.AnimationName, 0.1f);
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void Exit()
    {

    }
}
