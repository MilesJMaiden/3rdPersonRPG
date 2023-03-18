using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine //Right Side of the colon is what you inherit from
{

    [field: SerializeField] public InputReader InputReader { get; private set; } //rules get - you can publicly get the input reader. private set - ONLY privately can this be set // //Pascal case

    // Start is called before the first frame update
    void Start()
    {
        SwitchState(new PlayerTestState(this));
    }
}
