using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum BulletInputHelper { Reload, Shoot, Aim }

[RequireComponent(typeof(PlayerInput))]
public abstract class BulletInput : MonoBehaviour
{
    protected PlayerInput playerInput;

    protected virtual void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        playerInput.actions[BulletInputHelper.Reload.ToString()].performed += Reload;
        playerInput.actions[BulletInputHelper.Shoot.ToString()].performed += Shoot;

        playerInput.actions[BulletInputHelper.Reload.ToString()].canceled += Reload;
        playerInput.actions[BulletInputHelper.Shoot.ToString()].canceled += Shoot;
    }

    protected abstract void Reload(InputAction.CallbackContext value);
    protected abstract void Shoot(InputAction.CallbackContext value);
}
