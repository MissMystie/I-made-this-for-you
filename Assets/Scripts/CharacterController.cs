using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class CharacterController : MonoBehaviour
{
    public Transform sprite;
    public Animator anim;
    public int faceDir = 1;

    private Controls controls;
    public Rigidbody2D rb;
    public float gravity = 9.8f;
    public float moveSpeed = 5f;
    public float jumpVelocity = 8f;
    public float jumpDamp = 0.8f;
    public float jumpBuildupTime = 2f / 8;
    public float jumpFreezeTime = 4f / 8;
    public float drag = 0.1f;
    public float acc = 0.1f;
    public float airDrag = 0.1f;
    public float airAcc = 0.1f;
    protected bool canMove;

    public Vector2 move;
    public bool isGrounded;
    public float groundCheckDist = 0.01f;
    public LayerMask groundMask;

    public Collider2D groundCollider;
    public Collider2D movingPlatform;
    public string movingPlatformTag = "Moving";

    public bool controlsEnabled;

    public string speedAnimParam = "speed";
    public string attackAnimParam = "attack";
    public string jumpAnimParam = "jump";
    public string groundedAnimParam = "grounded";

    public virtual void Awake()
    {
        canMove = true;
    }

    public void Update()
    {
        if (canMove)
        {
            if (move.x != 0) faceDir = (int)Mathf.Sign(move.x);

            Vector2 scale = transform.localScale;
            scale.x = faceDir * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        anim.SetBool(groundedAnimParam, isGrounded);
        anim.SetFloat(speedAnimParam, Mathf.Abs(rb.linearVelocityX));
    }

    public void FixedUpdate()
    {
        bool wasGrounded = isGrounded;
        float hitDist = rb.linearVelocityY < 0 ? Mathf.Abs(rb.linearVelocityY) * Time.fixedDeltaTime : 0;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up * (groundCheckDist / 2),
            -transform.up, groundCheckDist, groundMask);

        if (hit) groundCollider = hit.collider;
        else groundCollider = null;

        isGrounded = groundCollider != null;

        //if (!isGrounded) rb.linearVelocityY -= gravity * Time.fixedDeltaTime;
        //else if (rb.linearVelocityY < 0) rb.linearVelocityY = 0f;

        if (!wasGrounded && isGrounded)
        {
            OnGrounded(groundCollider);
        }

        if (canMove)
        {
            if (controlsEnabled && controls != null) move = controls.Player.Move.ReadValue<Vector2>();

            if (move.x != 0) rb.linearVelocityX += move.x * moveSpeed * (isGrounded ? acc : airAcc);
            else rb.linearVelocityX *= (1 - (isGrounded ? drag : airDrag));
        }

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


        }
    }

    public IEnumerator JumpCoroutine()
    {
        move.x = 0;
        rb.linearVelocityX = 0;
        anim.SetTrigger(jumpAnimParam);

        canMove = false;

        yield return new WaitForSeconds(jumpBuildupTime);

        rb.linearVelocityY = jumpVelocity;

        yield return new WaitForSeconds(jumpFreezeTime - jumpBuildupTime);

        canMove = true;
        if (rb.linearVelocityY > 0) rb.linearVelocityY *= (1 - jumpDamp);
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

    public void OnGrounded(Collider2D platform)
    {

        /*else
        {
            movingPlatform = null;
            transform.parent = null;
        }*/
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, groundCheckDist);
    }
}
