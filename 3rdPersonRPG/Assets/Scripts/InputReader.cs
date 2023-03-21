using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// CTRL + . To implement method after adding to Unity Input
public class InputReader : MonoBehaviour, Controls.IPlayerActions //Interface
{
    public Vector2 movementValue { get; private set; }

    public event Action JumpEvent;
    public event Action DodgeEvent;

    public event Action TargetEvent;
    public event Action CancelEvent;
    

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
        JumpEvent?.Invoke();       
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; } //Performed = pressed (Left Ctrl)
        //If nobody is subcribed to an event, an error can occur
        DodgeEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if(!context.performed) { return; }

        TargetEvent?.Invoke();
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        CancelEvent?.Invoke();
    }
}
