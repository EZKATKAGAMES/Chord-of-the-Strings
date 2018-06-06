using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatConductor : MonoBehaviour
{
    /// This script will be responsible for activating
    /// combat abilities for the player.
    /// 

    #region Melee Variables
    public float meleeDamage;
    Melee meleeRef;



    #endregion

    #region Ability1: StarShot Variables
    public GameObject projectile;

    #endregion

    private void Awake()
    {
        meleeRef = GetComponentInChildren<Melee>();
        projectile = Resources.Load("Prefabs/Abilities/Ability1_Starshot") as GameObject;
    }

    void Start()
    {
       
    }

    
    void Update()
    {
        meleeRef.MeleeAttack();
    }

    void MeleeComboModifiers()
    {
        float defaultDamage = 10f;
        if (meleeRef.comboProgress == 0) // No combo
        {
            meleeDamage = defaultDamage;
        }
        else if(meleeRef.comboProgress == 1)
        {
            meleeDamage *= 0.3f;
        }
        else if(meleeRef.comboProgress == 2)
        {
            meleeDamage *= 0.3f;
        }
        else if(meleeRef.comboProgress == 3)
        {
            meleeDamage *= 0.2f;
        }

    }

    private void FixedUpdate()
    {
        
    }

   


}
