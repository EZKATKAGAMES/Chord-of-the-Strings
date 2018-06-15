using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatConductor : MonoBehaviour
{
    PlayerCharacter mouseInfoRef;
    [Header("Combat")]

    [Range(1, 4)]
    public static int selectedSpell = 1;

    #region Melee Variables
    [Header("Melee")]
    public float meleeDamage;
    public float knockForce;
    Melee meleeRef;
    #endregion

    #region Ability1: StarShot Variables
    [Header("Ability1 Variables")]
    public GameObject ability1;
    public StarShot starShotRef;
    //public float a1Damage;
    public bool a1_spellActive;
    public bool a1_readyToActivate = true; // Cooldown for ability
    public bool a1_readyToFire = false; // Cooldown for firing projectiles
    public bool a1_chargeReady;
    [Header("   ")] // Inspector aesthetics!!!
    public float a1_timer1 = 0; // Counts towards the cooldown limit
    public float a1_ability1CooldownLimit = 0.6f; // Threshold we must reach before we can cast the spell again
    [Header("   ")] // Inspector aesthetics!!!
    public float a1_timer2 = 0; // Counts towards the fire delay
    public float a1_delayBetweenFire = 0.3f; // The time we must wait before firing another shot.
    [Header("   ")] // Inspector aesthetics!!!
    public float a1_timer3 = 0; // Charge this up to cast charged ability1
    public float a1_chargeLimit = 3.0f;
    #endregion

    #region Ability2: RadiantSun Variables
    [Header("   ")] // Inspector aesthetics!!!
    [Header("Ability2 Variables")]
    public GameObject ability2;
    public RadiantSun radiantSunRef;
    public bool a2_active;
    public bool a2_readyToActivate = true;
    [Header("   ")] // Inspector aesthetics!!!
    public float a2_timer1;
    public float a2_activeTimeLimit;
    [Header("   ")] // Inspector aesthetics!!!
    public float a2_timer2;
    public float a2_coolDownLimit;
    [Tooltip("Damage Per Second")]
    public float a2_damage = 2f;

    #endregion

    private void Awake()
    {
        meleeRef = GetComponentInChildren<Melee>();
        ability1 = Resources.Load("Prefabs/Abilities/Ability1_Starshot") as GameObject;
        ability2 = Resources.Load("Prefabs/Abilities/Ability2_RadiantSun") as GameObject;
    }

    void Start()
    {
       
    }

    
    void Update()
    {
        AbilitySelection();
        meleeRef.MeleeAttack();

        Ability1Handling();
        Ability2Handling();
    }

    void AbilitySelection()
    {
        if (Input.GetKeyDown(GameManager.GM.Ab1))
        {
            selectedSpell = 1;
        }
        else if (Input.GetKeyDown(GameManager.GM.Ab2))
        {
            selectedSpell = 2;
        }
        else if (Input.GetKeyDown(GameManager.GM.Ab3))
        {
            selectedSpell = 3;
        }
        else if (Input.GetKeyDown(GameManager.GM.Ab4))
        {
            selectedSpell = 4;
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
        if(starShotRef == null)
        {
            a1_readyToFire = false;
            a1_spellActive = false;
            a1_timer2 = 0;
            a1_timer3 = 0;
        }

        if(selectedSpell != 1 && a1_spellActive)
        {
            a1_spellActive = false;
            a1_readyToFire = false;
        }

        // Ability Functionality
        if (selectedSpell == 1)
        {
           // If we press rightmouse button once we have the spell selected.
           if (Input.GetMouseButtonDown(1) && a1_readyToActivate == true)
           {
                // Activate the spell
                a1_spellActive = true;
                Instantiate(ability1, gameObject.transform);
                StartCoroutine(DelayAfterActivation());
                a1_readyToActivate = false;
           }
           // Charging
           if (starShotRef != null)
           {
               if (Input.GetMouseButton(1) && starShotRef.charges > 0 && a1_spellActive)
                    a1_timer3 += Time.deltaTime;
               else if (Input.GetMouseButtonUp(1)) // Reset on button release
                    a1_timer3 = 0;
                // Allow charge attacks
                if (a1_timer3 >= a1_chargeLimit)
                {
                    a1_chargeReady = true;
                    a1_timer3 = 0;
                }
            }           
           // If we release fire button and charge timer exceeds the limit, do a charge attack
           if (a1_readyToFire == true && a1_spellActive == true && a1_chargeReady == true && starShotRef.charges > 0 && Input.GetMouseButtonUp(1))
           {
                starShotRef.HoldFire();
                a1_timer3 = 0;   
           }
           // if not then do a regular attack
           if(a1_readyToFire == true && a1_spellActive == true && a1_chargeReady == false && starShotRef.charges > 0 && Input.GetMouseButtonUp(1))
           {   
                starShotRef.PressFire();
                a1_timer3 = 0;
           }         
        }
        #region Cooldowns
        
        // Ability CD
        if(a1_readyToActivate == false && a1_spellActive == false)
        {
            a1_timer1 += Time.deltaTime;
            if(a1_timer1 >= a1_ability1CooldownLimit)
            {
                a1_readyToActivate = true;
                a1_timer1 = 0;
            }
        }
        // Reset timer 2 
        if (a1_readyToFire == true)
            a1_timer2 = 0;
        // After the initial ready to fire
        if (a1_readyToFire == false && a1_spellActive == true)
        {
            a1_timer2 += Time.deltaTime;
            if (a1_timer2 >= a1_delayBetweenFire)
            {
                a1_readyToFire = true;
                a1_timer2 = 0;
            }
        }
        #endregion
    }

    // Initial ready to fire
    IEnumerator DelayAfterActivation()
    {
        yield return new WaitForSeconds(0.2f);
        a1_readyToFire = true;
    }

    public void GetStarShotRef()
    {
        // Reference the newly spawned prefab, which is a child object to us.
        starShotRef = gameObject.GetComponentInChildren<StarShot>();
        Debug.Log(starShotRef.name);
    }

    #endregion


    #region Ability2

    void Ability2Handling()
    {
        if(selectedSpell == 2)
        {
            if (Input.GetMouseButtonDown(1) && a2_readyToActivate == true)
            {
                Debug.Log("abilitiru two!!");
                Instantiate(ability2, transform);
                a2_active = true;
                a2_readyToActivate = false;
            }
        }

        #region Cooldown

        // If we just activated the spell
        if(a2_readyToActivate == false && a2_active == true)
        {
            a2_timer2 = 0;
            a2_timer1 += Time.deltaTime;
            if(a2_timer1 >= a2_activeTimeLimit)
            {
                a2_active = false;
                a2_timer1 = 0;
            }
        }
        // Start cooldown, spell has been used.
        if(a2_active == false && a2_readyToActivate == false)
        {
            a2_timer2 += Time.deltaTime;
            if (a2_timer2 >= a2_coolDownLimit)
            {
                a2_readyToActivate = true;
                a2_timer2 = 0;
            }
        }

        #endregion
    }

    public void GetRadiantSunRef()
    {
        // Reference the newly spawned prefab, which is a child object to us.
        radiantSunRef = gameObject.GetComponentInChildren<RadiantSun>();
        Debug.Log(radiantSunRef.name);
    }

    #endregion

}
