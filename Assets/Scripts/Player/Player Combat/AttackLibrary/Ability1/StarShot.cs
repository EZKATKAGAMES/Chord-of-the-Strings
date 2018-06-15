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
    // spawnLocation
    public GameObject origin;
    public Vector3 originVector;

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

        origin = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {      
        if(charges <= 0)
        {
            combatRef.a1_spellActive = false;
            combatRef.a1_readyToFire = false;
            Destroy(gameObject);
        }

        if (combatRef.a1_spellActive == false)
            Destroy(gameObject);
        
        originVector = origin.transform.position;
    }

    #region Firing

    public void PressFire()
    {
        // this will activate upon button release before the hold activates

        // Play animation
  
        Instantiate(a1_SmallProjectile, originVector, mouseInfoRef.MouseVectorInfo.aimRotation); 
        charges--;
        combatRef.a1_readyToFire = false;
    }
    
    public void HoldFire()
    {
        // this will activate upton button hold and release.

        // Play animation

     
        Instantiate(a1_LargeProjectile, originVector, mouseInfoRef.MouseVectorInfo.aimRotation); 
        combatRef.a1_chargeReady = false;
        charges = 0; 
    }

    #endregion

}




