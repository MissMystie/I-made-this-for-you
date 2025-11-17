using Mystie.Core;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public Rigidbody2D rb;
    public Action onDeath;
    public Action onRespawn;
    public Animator anim;

    public bool dieOnHit;
    public bool respawns;
    public Vector2 safeGround;
    public float respawnTime;
    public List<DamageType> vulnerabilities;
    public Mechanism mechanism;

    public enum DamageType { Knife, Water, Fire };

    public float deathDelay = 1 / 2f;
    public float respawnDelay = 1 / 2f;
    public string deathAnimParam = "death";
    public string respawnAnimParam = "respawn";

    void Start()
    {
        safeGround = transform.position;
    }

    public void TakeDamage(DamageType dmgType)
    {
        if (vulnerabilities.Contains(dmgType))
        {
            if (dieOnHit) StartCoroutine(OnDeath());
            else if (mechanism != null) mechanism.Activate(true);
        }
    }

    public IEnumerator OnDeath()
    {
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
        }

        onDeath?.Invoke();

        if (anim != null) anim.SetTrigger(deathAnimParam);

        yield return new WaitForSeconds(deathDelay);

        gameObject.SetActive(false);

        if (respawns) LevelManager.Instance.StartCoroutine(RespawnCoroutine());
    }

    public IEnumerator RespawnCoroutine()
    {
        gameObject.SetActive(false);
        gameObject.transform.position = safeGround;

        yield return new WaitForSeconds(respawnTime);

        gameObject.SetActive(true);
        if (anim != null) anim.SetTrigger(respawnAnimParam);

        yield return new WaitForSeconds(respawnDelay);

        onRespawn?.Invoke();
        if (rb != null) rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Respawn")) safeGround = collider.transform.position;
    }
}
