using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float bounce = 18f;
    public string target;
    public LayerMask mask;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == target)
        {
            Rigidbody2D rb = collider.attachedRigidbody;
            if (rb != null)
            {
                rb.linearVelocityY = bounce;
            }
        }
    }
}
