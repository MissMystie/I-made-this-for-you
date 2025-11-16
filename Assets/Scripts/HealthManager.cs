using Mystie.Core;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public bool dieOnHit;
    public bool respawns;
    public Vector2 safeGround;
    public float respawnTime;
    public List<DamageType> vulnerabilities;
    public Mechanism mechanism;

    public enum DamageType { Knife, Water, Fire  };

    void Start()
    {
        safeGround = transform.position;
    }

    public void TakeDamage(DamageType dmgType)
    {
        if (vulnerabilities.Contains(dmgType))
        {
            if (dieOnHit) OnDeath();
            else if (mechanism != null) mechanism.Activate(true);
        }
    }

    public void OnDeath()
    {
        gameObject.SetActive(false);

        if (respawns) LevelManager.Instance.StartCoroutine(RespawnCoroutine());
    }

    public IEnumerator RespawnCoroutine()
    {
        gameObject.SetActive(false);
        gameObject.transform.position = safeGround;

        yield return new WaitForSeconds(respawnTime);

        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Respawn")) safeGround = collider.transform.position;
    }
}
