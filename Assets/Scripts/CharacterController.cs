using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class CharacterController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpVelocity = 8f;
    public Controls controls;
    public Vector2 move;

    public bool controlsEnabled;

    public void Awake()
    {
        controls = new Controls();
        controls.Enable();
    }

    public void Update()
    {
        rb.linearVelocityX = move.x * moveSpeed;
    }

    public void Attack(CallbackContext ctx = default)
    {
        Debug.Log("attack");
    }

    private void Move(CallbackContext ctx = default)
    {
        Debug.Log("move");
        move = controls.Player.Move.ReadValue<Vector2>();
    }

    public void Choose()
    {

    }

    public void EnableInputs()
    {
        if (controlsEnabled) return;

        controls.Player.Jump.performed += Attack;
        controls.Player.Move.started += Move;

        move = Vector2.zero;

        controlsEnabled = true;
    }

    

    public void DisableInputs()
    {
        if (!controlsEnabled) return;

        controls.Player.Jump.performed -= Attack;
        controls.Player.Move.performed -= Move;

        move = Vector2.zero;

        controlsEnabled = false;
    }
}
