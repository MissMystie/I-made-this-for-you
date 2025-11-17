using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public string playerTag = "Player";
    public Collider2D player;
    public List<CharacterController> passengers;

    public Vector3 lastPos;

    public void OnEnable()
    {
        lastPos = transform.position;
    }

    public void FixedUpdate()
    {
        Vector3 delta = transform.position - lastPos;
        if (player != null)
        {
            player.transform.position += delta;
        }
        lastPos = transform.position;
    }

    public void OnDisable()
    {
        if (player != null) player.transform.parent = null;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag(playerTag)
            && !transform.IsChildOf(collider.transform))
        {
            player = collider;
            player.transform.parent = transform;
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (player != null && collider.gameObject == player.gameObject)
            Unparent();
    }

    public void Unparent()
    {
        if (player != null)
        {
            player.transform.parent = null;
            player = null;
        }
    }
}
