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

    void TakeDamage(int amount)
    {
        health -= damage; // Cast floats to int
    }

    void Death()
    {
        if (health <= 0) // Include necessary animation + particles later.
            Destroy(gameObject);
    }

    #region Detect Incoming Melee Damage
    private void OnTriggerEnter(Collider detectMelee)
    {
        if (detectMelee.transform.gameObject.layer == 11) // MeleeLayer = 11 (Unity Order)
        {
            Debug.Log(detectMelee.name);
            Debug.Log(detectMelee.GetComponentInParent<GameObject>().name);
            CombatConductor setDamage = detectMelee.gameObject.GetComponentInParent<CombatConductor>(); // Reference script to get values
            float damage = setDamage.meleeDamage; // Store value
            TakeDamage((int)damage); // Take Damage
            Debug.Log("ow");
        }
    }


    #endregion
}
