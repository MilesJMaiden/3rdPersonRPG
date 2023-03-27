using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{

    private readonly int attackHash = Animator.StringToHash("AttackE");

    private const float transitionDuration = 0.1f;

    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine){
}

    public override void Enter()
    {
        //FacePlayer();

        stateMachine.weapon.SetAttack(stateMachine.attackDamage, stateMachine.attackKnockback);

        stateMachine.animator.CrossFadeInFixedTime(attackHash, transitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        if(GetNormalizedTime(stateMachine.animator) >= 1) //How far we are through anim
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }

        FacePlayer();

    }

    public override void Exit()
    {

    }
}
