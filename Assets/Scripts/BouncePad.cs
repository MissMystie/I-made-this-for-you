using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float flow = 18f;
    public float bounce = 18f;
    public string target;
    public LayerMask mask;
    public List<Rigidbody2D> targets;

    public void FixedUpdate()
    {
        foreach (Rigidbody2D target in targets)
        {
            target.AddForceY(flow);
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (!targets.Contains(collider.attachedRigidbody) && collider.gameObject.name == target)
        {
            Rigidbody2D rb = collider.attachedRigidbody;

            if (rb != null)
            {
                targets.Add(rb);

                KnifeCharacterController knifeman = rb.GetComponent<KnifeCharacterController>();

                if (knifeman == null || !knifeman.platformOut)
                    rb.linearVelocityY = bounce;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (targets.Contains(collider.attachedRigidbody))
        {
            targets.Remove(collider.attachedRigidbody);
        }
    }
}
