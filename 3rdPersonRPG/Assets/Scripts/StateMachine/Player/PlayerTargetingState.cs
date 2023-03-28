using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int targetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetingForwardSpeedHash = Animator.StringToHash("TargetingForwardSpeed");
    private readonly int TargetingRightSpeedHash = Animator.StringToHash("TargetingRightSpeed");

    private const float crossFadeDuration = 0.1f;

    private Vector2 dodgingDirectionInput;

    private float remainingDodgeDuration;
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
        stateMachine.InputReader.CancelEvent += OnCancel;
        stateMachine.InputReader.DodgeEvent += OnDodge;

        stateMachine.animator.CrossFadeInFixedTime(targetingBlendTreeHash, crossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        //Debug.Log(stateMachine.Targeter.currentTarget.name);

        if(stateMachine.InputReader.isAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }

        if (stateMachine.InputReader.isBlocking)
        {
            stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
        }

        if(stateMachine.Targeter.currentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        Vector3 movement = CalculateMovement(deltaTime);

        Move(movement * stateMachine.targetingMovementSpeed, deltaTime);

        UpdateAnimator(deltaTime);


        FaceTarget();
    }

    public override void Exit()
    {
        stateMachine.InputReader.CancelEvent -= OnCancel;
        stateMachine.InputReader.DodgeEvent += OnDodge;
    }

    private void OnCancel()
    {
        stateMachine.Targeter.Cancel();

        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    private void OnDodge()
    {
        if(Time.time - stateMachine.previousDodgeTime < stateMachine.dodgeCooldown) { return;}

        stateMachine.SetDodgeTime(Time.time);
        //movement logic
        dodgingDirectionInput = stateMachine.InputReader.movementValue;
        remainingDodgeDuration = stateMachine.dodgeDuration;
    }

    private Vector3 CalculateMovement(float deltaTime)
    {
        Vector3 movement = new Vector3();

        if(remainingDodgeDuration > 0f)
        {
            movement += stateMachine.transform.right * dodgingDirectionInput.x * stateMachine.dodgeDistance / stateMachine.dodgeDuration;
            movement += stateMachine.transform.forward * dodgingDirectionInput.y * stateMachine.dodgeDistance / stateMachine.dodgeDuration;

            /*remainingDodgeDuration -= deltaTime;

            if(remainingDodgeDuration < 0f)
            {
                remainingDodgeDuration = 0f;
            }
            */ 
            //shorthand
            remainingDodgeDuration = MathF.Max(remainingDodgeDuration - deltaTime, 0f);
        }
        else
        {
            movement += stateMachine.transform.right * stateMachine.InputReader.movementValue.x;
            movement += stateMachine.transform.forward * stateMachine.InputReader.movementValue.y;
        }

        return movement;
    }
    private void UpdateAnimator(float deltaTime)
    {
        if(stateMachine.InputReader.movementValue.y == 0) {

            stateMachine.animator.SetFloat(TargetingForwardSpeedHash, 0, 0.1f, deltaTime);
        } 
        else
        {
            float value = stateMachine.InputReader.movementValue.y > 0 ? 1f : -1f; //if this returns true it uses the first value '1f' OR if returns false uses the second value '-1f'
            stateMachine.animator.SetFloat(TargetingForwardSpeedHash, value, 0.1f, deltaTime);
        }

        if (stateMachine.InputReader.movementValue.x == 0)
        {

            stateMachine.animator.SetFloat(TargetingRightSpeedHash, 0, 0.1f, deltaTime);
        }
        else
        {
            float value = stateMachine.InputReader.movementValue.x > 0 ? 1f : -1f; //if this returns true it uses the first value '1f' OR if returns false uses the second value '-1f'
            stateMachine.animator.SetFloat(TargetingRightSpeedHash, value, 0.1f, deltaTime);
        }
    }
}
