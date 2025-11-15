using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class CharacterController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpVelocity = 8f;
    
    public Vector2 move;
    public bool isGrounded;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundMask;

    public bool controlsEnabled;

    public void FixedUpdate()
    {
        rb.linearVelocityX = move.x * moveSpeed;

        isGrounded = Physics2D.OverlapCircle(transform.position, groundCheckRadius, groundMask);
    }

    public virtual void Attack(CallbackContext ctx = default)
    {
        Debug.Log("attack");
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocityY = jumpVelocity;
        }
    }

    private void Move(CallbackContext ctx = default)
    {
        Debug.Log("move");
        move = ctx.ReadValue<Vector2>();
    }

    public void Choose()
    {

    }

    public void EnableInputs(Controls controls)
    {
        if (controlsEnabled) return;

        controls.Player.Jump.performed += Attack;
        controls.Player.Move.started += Move;

        move = Vector2.zero;

        controlsEnabled = true;
    }

    

    public void DisableInputs(Controls controls)
    {
        if (!controlsEnabled) return;

        controls.Player.Jump.performed -= Attack;
        controls.Player.Move.performed -= Move;

        move = Vector2.zero;

        controlsEnabled = false;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(Vector2.zero, groundCheckRadius);
    }
}
