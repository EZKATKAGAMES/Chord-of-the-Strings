using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StarShot : MonoBehaviour
{
    // Reference to other scripts
    CombatConductor combatRef;
    PlayerCharacter mouseInfoRef;
    // Weapon Charges
    [HideInInspector]
    public int charges = 3;
    // Objects to instantiate
    GameObject a1_SmallProjectile;
    GameObject a1_LargeProjectile;

    public void Awake()
    {    
        a1_SmallProjectile = Resources.Load("Prefabs/Abilities/a1_SmallProjectile", typeof(GameObject)) as GameObject;
        a1_LargeProjectile = Resources.Load("Prefabs/Abilities/a1_LargeProjectile", typeof(GameObject)) as GameObject;
    }
    private void Start()
    {
        // In start becuase parent only exist once this object spawned (as child).
        combatRef = GetComponentInParent<CombatConductor>();
        mouseInfoRef = GetComponentInParent<PlayerCharacter>();
        combatRef.GetStarShotRef();  
    }
    private void Update()
    {      
        if(charges <= 0)
        {
            combatRef.spellActive = false;
            combatRef.readyToFire = false;
            Destroy(gameObject);
        }
    }

    #region Firing

    public void PressFire()
    {
        // this will activate upon button release before the hold activates

        // Play animation
  
        Instantiate(a1_SmallProjectile, Vector3.zero, mouseInfoRef.MouseVectorInfo.aimRotation); 
        charges--;
        combatRef.readyToFire = false;
    }
    
    public void HoldFire()
    {
        // this will activate upton button hold and release.

        // Play animation

     
        Instantiate(a1_LargeProjectile, Vector3.zero, mouseInfoRef.MouseVectorInfo.aimRotation); 
        combatRef.chargeReady = false;
        charges = 0; 
    }

    #endregion

}




