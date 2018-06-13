using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StarShot : MonoBehaviour
{
    [HideInInspector]
    public int projectiles;
    int rotatingProjectiles;

    [HideInInspector]
    public CombatConductor combatRef;

    Vector3 newProjectileInstanceLocation;

    // Projectile Properties
    public float projectileSpeed;
    public float lifeSpan;
    

    // Collider of projectiles // Maybe an array to get each collider??
    public SphereCollider[] colliders;
    Vector3 cursorLocation;

    public void Awake()
    {
        // Reference each collider.
        colliders = GetComponentsInChildren<SphereCollider>();

    }

    private void Start()
    {
        combatRef = GetComponentInParent<CombatConductor>();
        
        
    }

    private void Update()
    {
        // Check for how many projectiles are currently rotating the centriod.
        projectiles = GetComponentsInChildren<SphereCollider>().Length;        
    }

    public void Fire()
    {
        Debug.Log("Firing");
        // Get position of latest projectile in the array (if we are instantiating)
            //newProjectileInstanceLocation = colliders[projectiles-1].gameObject.transform.position;

        // Get the latest projectile in the array and translate them in the direction of the cursor.
        colliders[projectiles - 1].gameObject.GetComponentInChildren<ProjectileRotation>(true).stopRotating = true; // Stop rotation
        colliders[projectiles - 1].transform.Translate(Vector3.forward);

        
        
        
    }
    
    void projectile



}




