using UnityEngine;

public class Lifetime : MonoBehaviour
{
    public float lifetime = 2f;

    void Start()
    {
        Invoke("Kill", lifetime);
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
