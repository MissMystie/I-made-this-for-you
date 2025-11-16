using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class FireCharacterController : CharacterController
{
    public GameObject attackArea;
    public float attackTime = 0.5f;

    public override void Attack(CallbackContext ctx = default)
    {
        if (isGrounded && move.y < 0) StartCoroutine(JumpCoroutine());
        else StartCoroutine(AttackCoroutine());
    }

    public IEnumerator AttackCoroutine()
    {
        anim.SetTrigger(attackAnimParam);

        move.x = 0;
        rb.linearVelocityX = 0;
        canMove = false;
        attackArea.SetActive(true);

        yield return new WaitForSeconds(attackTime);

        canMove = true;
        attackArea.SetActive(false);
    }
}
