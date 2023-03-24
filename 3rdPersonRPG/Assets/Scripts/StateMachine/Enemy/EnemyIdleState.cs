using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{

    private readonly int locomotionBlendTreeHash = Animator.StringToHash("Locomotion");

    private readonly int speedHash = Animator.StringToHash("Speed");

    private const float crossFadeDuration = 0.1f;
    
    private const float animatorDampTime = 0.1f;
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
        stateMachine.animator.CrossFadeInFixedTime(locomotionBlendTreeHash, crossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        if(IsInDetectionRange())
        {

            Debug.Log("In Range of Player");
            //Transitioin to chase state
            return;
        }

        stateMachine.animator.SetFloat(speedHash, 0f, animatorDampTime, deltaTime);
    }

    public override void Exit(){}
}
