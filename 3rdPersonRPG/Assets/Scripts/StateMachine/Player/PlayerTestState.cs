using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

//This will be the framework for every state and this is where I will write the player logic
public class PlayerTestState : PlayerBaseState
{
    private float timer;
    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine) //call the base constructor passing in the needed state machine and caching it 
    {
    }

    public override void Enter()
    {
        //Debug.Log("Enter");
        //stateMachine.InputReader.jumpEvent += OnJump; //Subscribe
    }


    public override void Tick(float deltaTime)
    {
        //timer += deltaTime;
        //Debug.Log(timer);

        Vector3 movement = new Vector3();

        movement.x = stateMachine.InputReader.movementValue.x;
        movement.y = 0;
        movement.z = stateMachine.InputReader.movementValue.y;

        stateMachine.characterController.Move(movement * stateMachine.freeLookMovementSpeed * deltaTime);

        if(stateMachine.InputReader.movementValue == Vector2.zero) //If the player is not moving, we do not want to rotate them
        {
            stateMachine.animator.SetFloat("FreeLookSpeed", 0, 0.1f, deltaTime);
            return; 
        }

        stateMachine.animator.SetFloat("FreeLookSpeed", 1, 0.1f, deltaTime);
        stateMachine.transform.rotation = Quaternion.LookRotation(movement); //player will rotate in the movement direction

    }

    public override void Exit()
    {
        //Debug.Log("Exit");
        //stateMachine.InputReader.jumpEvent -= OnJump; //UNSubscribe
    }

    //private void OnJump()
    //{
        //stateMachine.SwitchState(new PlayerTestState(stateMachine));
    //}
}
