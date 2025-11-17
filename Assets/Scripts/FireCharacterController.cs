using FMODUnity;
using NUnit.Framework;
using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class FireCharacterController : CharacterController
{
    public GameObject attackArea;
    public float attackTime = 0.5f;


    public EventReference attackSFX;


    public override void Attack(CallbackContext ctx = default)
    {
        if (isGrounded && move.y > 0) Jump();
        else StartCoroutine(AttackCoroutine());
    }

    public IEnumerator AttackCoroutine()
    {
        anim.SetTrigger(attackAnimParam);

        move.x = 0;
        rb.linearVelocityX = 0;
        canMove = false;
        attackArea.SetActive(true);

        RuntimeManager.PlayOneShot(attackSFX);

        yield return new WaitForSeconds(attackTime);

        canMove = true;
        attackArea.SetActive(false);
    }
}
