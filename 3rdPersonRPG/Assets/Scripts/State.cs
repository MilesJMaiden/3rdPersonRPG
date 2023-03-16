using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//MonoBehaviour is a component to be on a GO (REMOVE)
public abstract class State //Abstract, you cannot create a state BUT you can inherit it
{
    public abstract void Enter(); //When Entering State //IF you are a State, you MUST have the below methods

    public abstract void Tick(float deltaTime); //Everyframe

    public abstract void Exit(); //When Exiting state

}

