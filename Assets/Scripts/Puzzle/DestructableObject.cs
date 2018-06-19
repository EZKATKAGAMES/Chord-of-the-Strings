using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    public float health = 20f;
    Collider collision;
    Renderer rend;


    private void Awake()
    {
        collision = GetComponent<BoxCollider>();
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        // Destroy the object once health reaches zero
        if(health <= 0)
        {
            collision.enabled = false;
            rend.enabled = false;
            // Insantiate effects
            Destroy(gameObject);
        }
    }

    // Take damage
    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    // Detect hits
    private void OnTriggerEnter(Collider other)
    {
        // This component is required by a projectile
        if (other.GetComponent<ProjectileTranslation>())
        {
            float dmg = other.GetComponent<ProjectileTranslation>().a1Damage;
            TakeDamage(dmg);
            // Instantiate particles
        }
    }
}
