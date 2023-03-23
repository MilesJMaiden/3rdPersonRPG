using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerAttackingState : PlayerBaseState
{
    private float previousFrameTime;

    private Attack attack;
    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine) //passes in extra parameter 'attackId' into contructor
    {
        attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        FaceTarget();

        float normalizedTime = GetNormalizedTime();
        previousFrameTime = normalizedTime;

        if (normalizedTime >= previousFrameTime && normalizedTime < 1f)
        {
            if(stateMachine.InputReader.isAttacking) {

                TryComboAttack(normalizedTime);             
            }
        }
        else
        {
            //go back to locomotion
        }
    }



    public override void Exit()
    {

    }

    private void TryComboAttack(float normalizedTime)
    {
        if(attack.ComboStateIndex == -1) { return; } //make sure we have a combo attack

        if(normalizedTime < attack.ComboAttackTime) { return; } //not ready to comboattack

        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, attack.ComboStateIndex));
    }

    private float GetNormalizedTime() //for anims
    {
        //Uses unity Anim TAGS
        //current state > next state
        AnimatorStateInfo currentInfo = stateMachine.animator.GetCurrentAnimatorStateInfo(0); //current info
        AnimatorStateInfo nextInfo = stateMachine.animator.GetNextAnimatorStateInfo(1); //Next anim info

        if(stateMachine.animator.IsInTransition(0) && nextInfo.IsTag("Attack")) //If we are transitioning into an attack
        {
            return nextInfo.normalizedTime; //how far through the animation
        }
        else if (!stateMachine.animator.IsInTransition(0) && currentInfo.IsTag("Attack")) //if not transition but playing attack animation
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0;
        }
    }
}
