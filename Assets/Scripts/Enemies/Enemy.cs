using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float damage;
    public float movementSpeed;
    public float knockForce;

    void Start()
    {
        
    }

   
    void Update()
    {
        Death();
    }

   public virtual void TakeDamage(float amount)
    {
        health -= amount; // Cast floats to int
    }

    public virtual void Death()
    {
        if (health <= 0) // Include necessary animation + particles later.
            Destroy(gameObject);
    }

}
