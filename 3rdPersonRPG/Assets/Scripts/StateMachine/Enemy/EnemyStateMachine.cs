using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public Animator animator { get; private set; }

    void Start()
    {
        SwitchState(new EnemyIdleState(this));
    }
}
