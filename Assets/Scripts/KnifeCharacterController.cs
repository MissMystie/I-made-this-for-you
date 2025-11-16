using FMODUnity;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class KnifeCharacterController : CharacterController
{
    public Transform throwPoint;
    public Rigidbody2D projectile;
    public float projectileSpeed = 6f;
    public GameObject platform;
    public bool platformOut;
    public float moveSpeedPlatform = 3f;
    public string boardAnimParam = "board";

    public EventReference throwSFX;
    

    public override void Awake()
    {
        base.Awake();
        platform.SetActive(false);
    }

    public override void Attack(CallbackContext ctx = default)
    {
        if (isGrounded)
        {
            if (move.y > 0 || platformOut)
            {
                platformOut = !platformOut;
                platform.SetActive(platformOut);
                anim.SetBool("board", platformOut);
            }
            
            if (!platformOut)
            {
                RuntimeManager.PlayOneShot(throwSFX);
                anim.SetTrigger(attackAnimParam);
                Rigidbody2D projectileInstance = Instantiate(projectile);
                projectileInstance.transform.position = throwPoint.position;
                projectileInstance.linearVelocityX = projectileSpeed * faceDir;
            }
        }
    }
}
