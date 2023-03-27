using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public Animator animator { get; private set; }

    [field: SerializeField] public CharacterController characterController { get; private set; }

    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }

    [field: SerializeField] public NavMeshAgent Agent { get; private set; }

    [field: SerializeField] public WeaponDamage weapon { get; private set; }

    [field: SerializeField] public Health health { get; private set; }

    [field: SerializeField] public Target target { get; private set; }

    [field: SerializeField] public float movementSpeed { get; private set; }

    [field: SerializeField] public float playerDetectionRange { get; private set; }

    [field: SerializeField] public float attackRange { get; private set; }

    [field: SerializeField] public int attackDamage { get; private set; }

    [field: SerializeField] public int attackKnockback { get; private set; }

    public GameObject player { get; private set; }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Agent.updatePosition = false;
        Agent.updateRotation = false;

        SwitchState(new EnemyIdleState(this));
    }

    private void OnEnable()
    {
        health.onTakeDamage += HandleTakeDamage;
        health.onDeath += HandleDeath;
    } 
    
    private void OnDisable()
    {
        health.onTakeDamage -= HandleTakeDamage;
        health.onDeath -= HandleDeath;
    }

    private void HandleTakeDamage()
    {
        SwitchState(new EnemyImpactState(this));
    }

    private void HandleDeath()
    {
        SwitchState(new EnemyDeadState(this));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRange);
    }


}
