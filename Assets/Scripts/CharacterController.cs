using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class CharacterController : MonoBehaviour
{
    private Controls controls;
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpVelocity = 8f;
    public float drag = 0.1f;
    public float acc = 0.1f;
    public float airDrag = 0.1f;
    public float airAcc = 0.1f;

    public Vector2 move;
    public bool isGrounded;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundMask;

    public bool controlsEnabled;

    public void FixedUpdate()
    {
        if (controlsEnabled && controls != null) move = controls.Player.Move.ReadValue<Vector2>(); 

        isGrounded = Physics2D.OverlapCircle(transform.position, groundCheckRadius, groundMask);

        if (move.x != 0) rb.linearVelocityX += move.x * moveSpeed * (isGrounded ? acc : airAcc);
        else rb.linearVelocityX *= (1 - (isGrounded ? drag : airDrag));

        rb.linearVelocityX = Mathf.Clamp(rb.linearVelocityX, -moveSpeed, moveSpeed);
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

    public void Choose()
    {

    }

    public void EnableInputs(Controls controls)
    {
        if (controlsEnabled) return;

        this.controls = controls;

        controls.Player.Jump.performed += Attack;

        move = Vector2.zero;

        controlsEnabled = true;
    }

    

    public void DisableInputs(Controls controls)
    {
        if (!controlsEnabled) return;

        controls.Player.Jump.performed -= Attack;

        move = Vector2.zero;

        controlsEnabled = false;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, groundCheckRadius);
    }
}
