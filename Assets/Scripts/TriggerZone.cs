using UnityEngine;
using static HealthManager;

public class TriggerZone : MonoBehaviour
{
    public bool killOnTrigger;
    public Lifetime lifetime;
    public DamageType damageType;

    void OnTriggerEnter2D(Collider2D col)
    {
        HealthManager health = col.GetComponent<HealthManager>();
        if (health != null)
        {
            health.TakeDamage(damageType);
            if (killOnTrigger) lifetime.Kill();
        }
    }
}
