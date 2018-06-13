﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatConductor : MonoBehaviour
{
    /// This script will be responsible for activating
    /// combat abilities for the player.
    /// 

    [Range(1, 4)]
    public int selectedSpell = 1;

    #region Melee Variables
    public float meleeDamage;
    Melee meleeRef;



    #endregion

    #region Ability1: StarShot Variables
    [Header("Ability1 Variables")]
    public GameObject ability1;
    public StarShot starShotRef;
    public bool spellActive;
    public bool readyToActivate = true; // Cooldown for ability
    public bool readyToFire = false; // Cooldown for firing projectiles
    [Header("   ")] // Inspector aesthetics!!!
    public float timer1 = 0; // Counts towards the cooldown limit
    public float timer2 = 0; // Counts towards the fire delay
    public float ability1CooldownLimit = 0.6f; // Threshold we must reach before we can cast the spell again
    public float delayBetweenFire = 0.3f; // The time we must wait before firing another shot.
    #endregion

    private void Awake()
    {
        meleeRef = GetComponentInChildren<Melee>();
        ability1 = Resources.Load("Prefabs/Abilities/Ability1_Starshot") as GameObject;
        
    }

    void Start()
    {
       
    }

    
    void Update()
    {
        meleeRef.MeleeAttack();
        Ability1Handling();

        if (spellActive)
        {
            //Invoke("GetStarShotRef", 0);
        }        

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

    #region Ability1 
    void Ability1Handling()
    {

        #region Cooldown

        // Cooldown between next activation of ability
        if(readyToActivate == false)
        {
            timer1 += Time.deltaTime;
            if(timer1 >= ability1CooldownLimit)
            {
                readyToActivate = true;
                timer1 = 0;
            }
        }

        // Cooldown between firing active ability
        if(readyToFire == false)
        {
            timer2 += Time.deltaTime;
            if(timer2 >= delayBetweenFire)
            {
                readyToFire = true;
                timer2 = 0;
            }
        }

        #endregion

        
       

        // If we have starshot selected.
        if (selectedSpell == 1)
        {
            // Activating Ability
            if (readyToActivate == true && Input.GetMouseButtonDown(1)) // RMB to activate spell
            {
                Instantiate(ability1, gameObject.transform);
                spellActive = true;
                readyToActivate = false;
                StartCoroutine(DelayAfterActivation());
            }

            // Firing
            if (spellActive == true && Input.GetMouseButtonDown(1) && readyToFire == true) // RMB while spell is active to fire projectiles
            {
                GetStarShotRef();
                starShotRef.Fire();
            }
        }

        // When we run out of projectiles
        if (starShotRef != null)
        {
            if (starShotRef.projectiles == 0 && spellActive == true)
                spellActive = false;
        }

    }





    IEnumerator DelayAfterActivation()
    {
        yield return new WaitForSeconds(0.2f);
        readyToFire = true;
    }

    

    void GetStarShotRef()
    {
        starShotRef = gameObject.GetComponentInChildren<StarShot>();
        Debug.Log(starShotRef.name);
    }

    #endregion

}
