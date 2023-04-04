using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{

    private readonly int fallHash = Animator.StringToHash("Fall");

    private Vector3 momentum;

    private const float crossFadeDuration = 0.3f;
    public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
        momentum = stateMachine.characterController.velocity;
        momentum.y = 0;

        stateMachine.animator.CrossFadeInFixedTime(fallHash, crossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(momentum, deltaTime);

        if(stateMachine.characterController.isGrounded)
        {
            ReturnToLocomotion();
        }

        FaceTarget();
    }

    public override void Exit()
    {

    }
}
