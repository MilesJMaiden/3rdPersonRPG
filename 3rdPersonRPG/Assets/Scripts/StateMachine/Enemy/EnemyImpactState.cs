using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpactState : EnemyBaseState
{

    private readonly int impactHash = Animator.StringToHash("Impact1");

    private const float crossFadeDuration = 0.1f;

    private float duration = 1f; //time spent in state

    public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
        stateMachine.animator.CrossFadeInFixedTime(impactHash, crossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        duration -= deltaTime;

        if(duration <= 0f)
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine)); //return to idle once done
        }
    }
    public override void Exit() { }
}
