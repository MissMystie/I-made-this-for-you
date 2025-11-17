using Mystie.Utils;
using UnityEngine;
using static HealthManager;

public class TriggerZone : MonoBehaviour
{
    public bool killOnTrigger;
    public Lifetime lifetime;
    public DamageType damageType;
    public LayerMask mask;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.IsInLayerMask(mask))
        {
            HealthManager health = col.GetComponent<HealthManager>();
            health?.TakeDamage(damageType);

            if (killOnTrigger) lifetime.Kill();
        }

    }
}
