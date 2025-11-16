using UnityEngine;

public class Cage : Mechanism
{
    public GameObject cage;
    public Collider2D ingredient;

    public override void Activate(bool on)
    {
        if (on)
        {
            cage.SetActive(false);
            ingredient.enabled = true;
            base.Activate(on);
        }
    }
}
