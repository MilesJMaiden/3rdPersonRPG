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

    [field: SerializeField] public float freeLookMovementSpeed { get; private set; }

    [field: SerializeField] public float playerRotationSpeed { get; private set; }

    public Transform mainCameraTransform { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        mainCameraTransform = Camera.main.transform;

        SwitchState(new PlayerFreeLookState(this));
    }
}
