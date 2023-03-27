using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
        //toggle ragodll
        //toggle ragodll
        stateMachine.ragdoll.ToggleRagdoll(true);

        //disable weapon
        stateMachine.weapon.gameObject.SetActive(false);

        //remove target
        GameObject.Destroy(stateMachine.target);

    }

    public override void Tick(float deltaTime){}

    public override void Exit(){}


}
