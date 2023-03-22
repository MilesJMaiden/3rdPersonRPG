using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int targetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetingForwardSpeedHash = Animator.StringToHash("TargetingForwardSpeed");
    private readonly int TargetingRightSpeedHash = Animator.StringToHash("TargetingRightSpeed");
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
        stateMachine.InputReader.CancelEvent += OnCancel;

        stateMachine.animator.Play(targetingBlendTreeHash);
    }

    public override void Tick(float deltaTime)
    {
        //Debug.Log(stateMachine.Targeter.currentTarget.name);

        if(stateMachine.Targeter.currentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        Vector3 movement = CalculateMovement();

        Move(movement * stateMachine.targetingMovementSpeed, deltaTime);

        UpdateAnimator(deltaTime);


        FaceTarget();
    }

    public override void Exit()
    {
        stateMachine.InputReader.CancelEvent -= OnCancel;
    }

    private void OnCancel()
    {
        stateMachine.Targeter.Cancel();

        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    private Vector3 CalculateMovement()
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * stateMachine.InputReader.movementValue.x;
        movement += stateMachine.transform.forward * stateMachine.InputReader.movementValue.y;

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
