using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public bool killOnTrigger;
    public Lifetime lifetime;

    void OnTriggerEnter2D(Collider2D col)
    {
        Mechanism mech = col.GetComponent<Mechanism>();
        if (mech != null && !mech.isOn)
        {
            mech.Toggle();

        }
    }
}
