using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgingState : PlayerBaseState
{
    private readonly int dodgeBlendTreeHash = Animator.StringToHash("DodgeBlend");
    private readonly int dodgeForwardHash = Animator.StringToHash("DodgeForward");
    private readonly int dodgeRightHash = Animator.StringToHash("DodgeRight");

    private Vector3 dodgingDirectionInput;

    private float remainingDodgeTime;

    private const float crossFadeDuration = 0.1f;

    //pass in direction to dodge withg statemachine
    public PlayerDodgingState(PlayerStateMachine stateMachine, Vector3 dodgingDirectionInput) : base(stateMachine)
    {
        this.dodgingDirectionInput = dodgingDirectionInput;
    }

    public override void Enter()
    {
        remainingDodgeTime = stateMachine.dodgeDuration;

        //these are normalized
        stateMachine.animator.SetFloat(dodgeForwardHash, dodgingDirectionInput.y);
        stateMachine.animator.SetFloat(dodgeRightHash, dodgingDirectionInput.x);

        stateMachine.animator.CrossFadeInFixedTime(dodgeBlendTreeHash, crossFadeDuration);

        stateMachine.health.SetInvulnerable(true);
    }
    public override void Tick(float deltaTime)
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * dodgingDirectionInput.x * stateMachine.dodgeDistance / stateMachine.dodgeDuration;
        movement += stateMachine.transform.forward * dodgingDirectionInput.y * stateMachine.dodgeDistance / stateMachine.dodgeDuration;

        Move(movement, deltaTime);

        FaceTarget();

        remainingDodgeTime -= deltaTime;

        //Run out of dodge time
        if(remainingDodgeTime <= 0f)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }

    }
    public override void Exit()
    {
        stateMachine.health.SetInvulnerable(false);
    }


}
