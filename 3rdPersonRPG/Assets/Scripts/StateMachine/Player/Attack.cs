using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] //marked class to let unity know it should be serialized for the inspector
public class Attack //data for each attack
{
    [field: SerializeField] public string AnimationName { get; private set; } // property
}
