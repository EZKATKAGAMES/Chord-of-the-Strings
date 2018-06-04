using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float damage;
    public float movementSpeed;


    void Start()
    {
        
    }

   
    void Update()
    {
        Death();
    }

   public void TakeDamage(int amount)
    {
        health -= amount; // Cast floats to int
    }

    void Death()
    {
        if (health <= 0) // Include necessary animation + particles later.
            Destroy(gameObject);
    }

}
