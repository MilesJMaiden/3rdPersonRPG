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
        stateMachine.InputReader.jumpEvent += OnJump; //Subscribe
    }


    public override void Tick(float deltaTime)
    {
        timer += deltaTime;

        Debug.Log(timer);

    }

    public override void Exit()
    {
        //Debug.Log("Exit");
        stateMachine.InputReader.jumpEvent -= OnJump; //UNSubscribe
    }

    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerTestState(stateMachine));
    }
}
