using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour //Stores current state that we are in & a way of switching between states
{
    [SerializeField] private State currentState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SwitchState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter(); //This does not NEED a null check BUT you can switch to NULL state.
    }

    // Update is called once per frame
    void Update()
    {
        /*if (currentState != null)
        {

        }*/
        //Shorthand below 
        currentState?.Tick(Time.deltaTime); //? - The 'null conditional operator' this will NOT work with a monobehaviour or scriptable object (use if)
    }


}
