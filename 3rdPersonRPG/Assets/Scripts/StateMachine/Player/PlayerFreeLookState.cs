using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

//This will be the framework for every state and this is where I will write the player logic
public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int freeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");

    private readonly int freeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");

    public float animatorSmoothTransition = 0.1f;

    private const float crossFadeDuration = 0.1f;
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) //call the base constructor passing in the needed state machine and caching it 
    {
    }

    public override void Enter()
    {
        //Debug.Log("Enter");
        //stateMachine.InputReader.jumpEvent += OnJump; //Subscribe

        stateMachine.InputReader.TargetEvent += OnTarget;

        stateMachine.animator.CrossFadeInFixedTime(freeLookBlendTreeHash, crossFadeDuration);
    }


    public override void Tick(float deltaTime)
    {
        //timer += deltaTime;
        //Debug.Log(timer);

        if (stateMachine.InputReader.isAttacking) //remove this if IF you want to only atack while targetting
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }

        Vector3 movement = CalculateMovement();

        Move(movement * stateMachine.freeLookMovementSpeed, deltaTime);

        if (stateMachine.InputReader.movementValue == Vector2.zero) //If the player is not moving, we do not want to rotate them
        {
            stateMachine.animator.SetFloat(freeLookSpeedHash, 0, animatorSmoothTransition, deltaTime);
            return;
        }

        stateMachine.animator.SetFloat(freeLookSpeedHash, 1, animatorSmoothTransition, deltaTime);
        FaceMovementDirection(movement, deltaTime);

    }

    public override void Exit()
    {
        //Debug.Log("Exit");
        //stateMachine.InputReader.jumpEvent -= OnJump; //UNSubscribe

        stateMachine.InputReader.TargetEvent -= OnTarget;
    }

    private void OnTarget()
    {
        if(!stateMachine.Targeter.SelectTarget()) { return; }

        //Only enters the targetting state if we succesfully select a target

        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }

    private Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.mainCameraTransform.forward;
        Vector3 right = stateMachine.mainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.movementValue.y + right * stateMachine.InputReader.movementValue.x;
    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(movement), deltaTime * stateMachine.playerRotationSpeed); //player will rotate in the movement direction
    }
}
