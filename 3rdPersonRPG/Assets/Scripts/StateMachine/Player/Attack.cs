using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] //marked class to let unity know it should be serialized for the inspector
public class Attack //data for each attack
{
    [field: SerializeField] public string AnimationName { get; private set; } // property

    [field: SerializeField] public float TransitionDuration { get; private set; } // property

    [field: SerializeField] public int ComboStateIndex { get; private set; } = -1; // property

    [field: SerializeField] public float ComboAttackTime { get; private set; } // property

    [field: SerializeField] public float ForceTime { get; private set; } // property

    [field: SerializeField] public float Force { get; private set; } // property
}
