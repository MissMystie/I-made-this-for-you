using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class FireCharacterController : CharacterController
{
    public override void Attack(CallbackContext ctx = default)
    {
        if (move.y < 0) Jump();
    }
}
