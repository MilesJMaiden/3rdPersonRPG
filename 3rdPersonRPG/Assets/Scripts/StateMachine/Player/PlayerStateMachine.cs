using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine //Right Side of the colon is what you inherit from
{

    [field: SerializeField] public InputReader InputReader { get; private set; } //rules get - you can publicly get the input reader. private set - ONLY privately can this be set // //Pascal case

    [field: SerializeField] public CharacterController characterController { get; private set; }

    [field: SerializeField] public Animator animator { get; private set; }

    [field: SerializeField] public Targeter Targeter { get; private set; }

    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }

    [field: SerializeField] public WeaponDamage Weapon { get; private set; }

    [field: SerializeField] public Health health { get; private set; }

    [field: SerializeField] public Ragdoll ragdoll { get; private set; }

    [field: SerializeField] public float freeLookMovementSpeed { get; private set; }

    [field: SerializeField] public float targetingMovementSpeed { get; private set; }

    [field: SerializeField] public float playerRotationSpeed { get; private set; }

    [field: SerializeField] public float dodgeDuration { get; private set; }

    [field: SerializeField] public float dodgeDistance { get; private set; }

    [field: SerializeField] public float dodgeCooldown { get; private set; }

    [field: SerializeField] public Attack[] Attacks { get; private set; }

    public Transform mainCameraTransform { get; private set; }

    public float previousDodgeTime { get; private set; } = Mathf.NegativeInfinity;

    // Start is called before the first frame update
    void Start()
    {
        mainCameraTransform = Camera.main.transform;

        SwitchState(new PlayerFreeLookState(this));
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
        SwitchState(new PlayerImpactState(this));
    }

    private void HandleDeath()
    {
        SwitchState(new PlayerDeadState(this));
    }

    public void SetDodgeTime(float dodgeTime)
    {
        previousDodgeTime = dodgeTime;
    }
}
