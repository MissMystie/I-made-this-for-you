using FMODUnity;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class KnifeCharacterController : CharacterController
{
    public Transform throwPoint;
    public Rigidbody2D projectile;
    public float projectileSpeed = 6f;
    public float projectileRotationSpeed = 45f;
    public MovingPlatform platform;
    public bool platformOut;
    public float moveSpeedPlatform = 3f;
    public string boardAnimParam = "board";

    public EventReference throwSFX;
    public EventReference boardUpSFX;
    public EventReference boardDownSFX;

    public override void Awake()
    {
        base.Awake();
        platform.gameObject.SetActive(false);
    }

    public override void Board(CallbackContext ctx = default)
    {
        if (isGrounded)
        {
            if (move.y < 0 || platformOut)
            {
                TogglePlatform();
                return;
            }

            StartCoroutine(JumpCoroutine());
        }
    }

    public override void Jump(CallbackContext ctx = default)
    {
        if (isGrounded)
        {
            if (platformOut)
            {
                TogglePlatform();
            }
            if (move.y < 0 || platformOut)
            {
                TogglePlatform();
                return;
            }

            StartCoroutine(JumpCoroutine());
        }
    }

    public override void Attack(CallbackContext ctx = default)
    {
        if (platformOut)
        {
            TogglePlatform();
        }
        if (!platformOut) Throw();
    }

    public void TogglePlatform()
    {
        platformOut = !platformOut;
        if (!platformOut) platform.Unparent();
        platform.gameObject.SetActive(platformOut);
        anim.SetBool(boardAnimParam, platformOut);
    }

    public void Throw()
    {
        RuntimeManager.PlayOneShot(throwSFX);
        anim.SetTrigger(attackAnimParam);
        Rigidbody2D projectileInstance = Instantiate(projectile);

        projectileInstance.transform.position = throwPoint.position;
        projectileInstance.linearVelocityX = projectileSpeed * faceDir;

        Vector2 scale = projectileInstance.transform.localScale;
        scale.x = Mathf.Sign(faceDir) * Mathf.Abs(scale.x);
        projectileInstance.transform.localScale = scale;
        //projectileInstance.angularVelocity = faceDir * projectileRotationSpeed;
    }
}
