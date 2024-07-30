using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 Movement { get; private set; }
    public Vector3 MousePos { get; private set; }
    public event Action<bool> OnJumpEvent;

    private void Update()
    {
        CheckMovement();
        CheckJumpInput();
        UpdateMousePosition();
    }

    private void UpdateMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        MousePos = mousePosition;
    }

    private void CheckJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpEvent?.Invoke(true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {

            OnJumpEvent?.Invoke(false);

        }
    }

    private void CheckMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Movement = (new Vector2(x, y)).normalized;
    }
}
