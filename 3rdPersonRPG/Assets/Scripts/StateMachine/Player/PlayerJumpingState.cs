using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    private readonly int jumpHash = Animator.StringToHash("Jump");

    private Vector3 momentum;

    private const float crossFadeDuration = 0.3f;
    public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.ForceReceiver.Jump(stateMachine.jumpForce);

        momentum = stateMachine.characterController.velocity;
        momentum.y = 0f;

        stateMachine.animator.CrossFadeInFixedTime(jumpHash, crossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(momentum, deltaTime);

        if (stateMachine.characterController.velocity.y <= 0) 
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            return; 
        }

        FaceTarget();
    }

    public override void Exit()
    {

    }
}
