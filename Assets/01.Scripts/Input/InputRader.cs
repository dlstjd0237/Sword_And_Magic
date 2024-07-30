using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName ="SO/InputRader")]
public class InputRader : ScriptableObject, Console.IFloorActions
{
    private Console _console;
    public Console Console => _console;

    public Action AttackEvent;

    private void OnEnable()
    {
        if (_console == null)
        {
            _console = new Console();
            _console.Floor.SetCallbacks(this);
        }
        _console.Floor.Enable();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            AttackEvent?.Invoke();
    }
}
