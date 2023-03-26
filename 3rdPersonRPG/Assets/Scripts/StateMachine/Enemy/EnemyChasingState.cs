using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{

    private readonly int locomotionBlendTreeHash = Animator.StringToHash("Locomotion");

    private readonly int speedHash = Animator.StringToHash("Speed");

    private const float crossFadeDuration = 0.1f;

    private const float animatorDampTime = 0.1f;
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.animator.CrossFadeInFixedTime(locomotionBlendTreeHash, crossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {

        if (!IsInDetectionRange())
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return;
        }
        else if (IsInAttackRange())
        {
            stateMachine.SwitchState(new EnemyAttackingState(stateMachine));
            return;
        }

        MoveToPlayer(deltaTime);

        FacePlayer();

        stateMachine.animator.SetFloat(speedHash, 1f, animatorDampTime, deltaTime);
    }

    private bool IsInAttackRange()
    {
        float playerDistanceSqr = (stateMachine.player.transform.position - stateMachine.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.attackRange * stateMachine.attackRange;
    }

    private void MoveToPlayer(float deltaTime)
    {
        stateMachine.Agent.destination = stateMachine.player.transform.position;

        Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.movementSpeed, deltaTime);

        stateMachine.Agent.velocity = stateMachine.characterController.velocity;    
    }

    public override void Exit() 
    {
        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity = Vector3.zero;
    }
}
