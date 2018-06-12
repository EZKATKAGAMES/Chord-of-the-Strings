using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StarShot : MonoBehaviour
{
    [HideInInspector]
    public int projectiles;

    Vector3 newProjectileInstanceLocation;

    // Projectile Properties
    public float projectileSpeed;
    public float lifeSpan;
    

    // Collider of projectiles // Maybe an array to get each collider??
    public SphereCollider[] colliders;


    public void Awake()
    {
        // Reference each collider.
        colliders = GetComponentsInChildren<SphereCollider>();

    }

    private void Start()
    {
        
        
        
    }

    private void Update()
    {
        // Check for how many projectiles are currently rotating the centriod.
        projectiles = GetComponentsInChildren<SphereCollider>().Length;        
    }

    public void Fire()
    {
        Debug.Log("Firing");
        // Get position of object before it deactivates
        newProjectileInstanceLocation = colliders[projectiles-1].gameObject.transform.position;
        
        
    }
    



}




