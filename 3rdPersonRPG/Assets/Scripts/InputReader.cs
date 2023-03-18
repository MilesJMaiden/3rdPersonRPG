using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions //Interface
{
    public event Action jumpEvent;
    public event Action dodgeEvent;

    private Controls controls;

    void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this); //Makes a link between this class and the OnJump Class

        controls.Player.Enable();
    }

    private void OnDestroy() //when object is destoryed or game not being played
    {
        controls.Player.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(!context.performed) { return; } //Performed = pressed (Spacebar or Button South)
        //If nobody is subcribed to an event, an error can occur
        jumpEvent?.Invoke();       
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; } //Performed = pressed (Left Ctrl)
        //If nobody is subcribed to an event, an error can occur
        dodgeEvent?.Invoke();
    }
}