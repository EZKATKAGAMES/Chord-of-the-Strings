using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StarShot : MonoBehaviour
{
    [HideInInspector]
    public CombatConductor combatRef;
    
    // Projectile Properties
    public float projectileSpeed;
    public float lifeSpan;

    int charges = 3;

    GameObject a1_SmallProjectile;
    GameObject a1_LargeProjectile;


    public void Awake()
    {
        a1_SmallProjectile = Resources.Load("Prefabs/Abilities/a1_SmallProjectile", typeof(GameObject)) as GameObject;
        a1_LargeProjectile = Resources.Load("Prefabs/Abilities/a1_LargeProjectile", typeof(GameObject)) as GameObject;
    }

    private void Start()
    {
        // In start becuase parent only exist once this object spawned.
        combatRef = GetComponentInParent<CombatConductor>();
        combatRef.GetStarShotRef();
        
    }

    private void Update()
    {
           
    }

    public void PressFire()
    {
        // this will activate upon button release before the hold activates

        // Play animation

        


        Instantiate(a1_SmallProjectile); // direction = mouse direction
        charges--;
    }
    
    public void HoldFire()
    {
        // this will activate upton button hold and release.

        // Play animation


        

        Instantiate(a1_LargeProjectile); // direction = mouse direction
        charges = 0;
    }
   



}




