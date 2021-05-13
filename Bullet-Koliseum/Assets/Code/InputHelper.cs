using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum InputHelperActions { Action, Aim, Jump, Move, Reload, Shoot, PickUp, Drop}

[RequireComponent(typeof(PlayerInput))]
public abstract class InputHelper : MonoBehaviour
{
    protected PlayerInput playerInput;

    protected virtual void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        playerInput.actions[InputHelperActions.Action.ToString()].performed += Action;
        playerInput.actions[InputHelperActions.Jump.ToString()].performed += Jump;
        playerInput.actions[InputHelperActions.Move.ToString()].performed += Move;
        playerInput.actions[InputHelperActions.PickUp.ToString()].performed += PickUp;
        playerInput.actions[InputHelperActions.Drop.ToString()].performed += Drop;

        playerInput.actions[InputHelperActions.Action.ToString()].canceled += Action;
        playerInput.actions[InputHelperActions.Jump.ToString()].canceled += Jump;
        playerInput.actions[InputHelperActions.Move.ToString()].canceled += Move;
        playerInput.actions[InputHelperActions.PickUp.ToString()].canceled += PickUp;
        playerInput.actions[InputHelperActions.Drop.ToString()].canceled += Drop;
    }

    protected abstract void Action(InputAction.CallbackContext value);
    protected abstract void Jump(InputAction.CallbackContext value);
    protected abstract void Move(InputAction.CallbackContext value);
    protected abstract void PickUp(InputAction.CallbackContext value);
    protected abstract void Drop(InputAction.CallbackContext value);
}
