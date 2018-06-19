using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatConductor : MonoBehaviour
{
    PlayerCharacter mouseInfoRef;
    [Header("Combat")]

    [Range(1, 4)]
    public static int selectedSpell = 1;

    // -1 is No Layer
    enum Layers {Default, TransparentFX, IgnoreRaycast, NULL3,Water, UI, PostProcessing, NULL7, CameraRayOnly,Collision, Enemy, PlayercastMelee, PlayerProjectiles, Passable, Ascended, Player};
    Layers gameObjectLayers;

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

    #region Ability3: GravityLock Variables
    [Header("   ")] // Inspector aesthetics!!!
    [Header("Ability3 Variables")]
    public GameObject ability3;
    public SpatialSuspension spatialSuspensionRef;
    public bool a3_readyToActivate = true;
    public bool a3_active;
    [Header("   ")] // Inspector aesthetics!!!
    public float a3_timer1;
    public float a3_durationLimit; // Duration object is active, not the effect it applies!!
    [Header("   ")] // Inspector aesthetics!!!
    public float a3_timer2;
    public float a3_cooldownLimit;
    #endregion


    #region Ability4: Ascension Variables
    [Header("   ")] // Inspector aesthetics!!!
    [Header("Ability4 Variables")]
    public GameObject ability4;
    public Ascension ascensionRef;
    public bool a4_readyToActivate = true;
    public bool a4_active;
    [Header("   ")] // Inspector aesthetics!!!
    public float a4_timer1;
    public float a4_durationLimit;
    [Header("   ")] // Inspector aesthetics!!!
    public float a4_timer2;
    public float a4_coolDownLimit;
    #endregion

    private void Awake()
    {
        meleeRef = GetComponentInChildren<Melee>();
        ability1 = Resources.Load("Prefabs/Abilities/Ability1_Starshot") as GameObject;
        ability2 = Resources.Load("Prefabs/Abilities/Ability2_RadiantSun") as GameObject;
        ability3 = Resources.Load("Prefabs/Abilities/Ability3_SpatialSuspension") as GameObject;
        ability4 = Resources.Load("Prefabs/Abilities/Ability4_Ascension") as GameObject;
    }
    
    void Update()
    {
        AbilitySelection();
        meleeRef.MeleeAttack();

        Ability1Handling();
        Ability2Handling();
        Ability3Handling();
        Ability4Handling();
    }

    public void GetStarShotRef()
    {
        // Reference the newly spawned prefab, which is a child object to us.
        starShotRef = gameObject.GetComponentInChildren<StarShot>();
        Debug.Log(starShotRef.name);
    }
    public void GetSpacialSuspensionRef()
    {
        // Reference the newly spawned prefab, which is a child object to us.
        spatialSuspensionRef = gameObject.GetComponentInChildren<SpatialSuspension>();
        Debug.Log(spatialSuspensionRef.name);
    }
    public void GetRadiantSunRef()
    {
        // Reference the newly spawned prefab, which is a child object to us.
        radiantSunRef = gameObject.GetComponentInChildren<RadiantSun>();
        Debug.Log(radiantSunRef.name);
    }
    public void GetAscensionRef()
    {
        // Reference the newly spawned prefab, which is a child object to us.
        ascensionRef = gameObject.GetComponentInChildren<Ascension>();
        Debug.Log(ascensionRef.name);
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
    #endregion

    #region Ability3
    void Ability3Handling()
    {
        if(selectedSpell == 3)
        {
            // If we are able to cast, do it!
            if(Input.GetMouseButtonDown(1) && a3_readyToActivate == true)
            {
                Debug.Log("ability thREEEEEEEE");
                Instantiate(ability3, transform);
                a3_active = true;
                a3_readyToActivate = false;
            }

            #region Cooldown
            // If ability is active, start duration coutner.
            if (a3_active == true && a3_readyToActivate == false)
            {
                a3_timer2 = 0;
                a3_timer1 += Time.deltaTime;
                if(a3_timer1 >= a3_durationLimit)
                {
                    a3_active = false;
                    a3_timer1 = 0;
                }
            }
            // if the ability duration is up, start cooldown.
            else if(a3_active == false && a3_readyToActivate == false)
            {
                a3_timer2 += Time.deltaTime;
                if(a3_timer2 >= a3_cooldownLimit)
                {
                    a3_readyToActivate = true;
                    a3_timer2 = 0;
                }
            }
            #endregion

        }
    }
    #endregion

    #region Ability4

    void Ability4Handling()
    {
        if(selectedSpell == 4)
        {
            if(Input.GetMouseButtonDown(1) && a4_readyToActivate == true)
            {
                a4_active = true;
                Instantiate(ability4, transform);
                a4_readyToActivate = false;
            }
            // If spell is active
            if(a4_active == true && a4_readyToActivate == false)
            {
                a4_timer2 = 0;
                // StartCooldown
                a4_timer1 += Time.deltaTime;
                if(a4_timer1 >= a4_durationLimit)
                {
                    a4_active = false;
                    a4_timer1 = 0;
                }
            }
            // If spell is inactive, start cooldown
            if(a4_active == false && a4_readyToActivate == false)
            {
                a4_timer2 += Time.deltaTime;
                if(a4_timer2 >= a4_coolDownLimit)
                {
                    a4_readyToActivate = true;
                    a4_timer2 = 0;
                }
            }

        }

        LayerEdit();
    }

    void LayerEdit()
    {
        if (a4_active)
        {
            gameObject.layer = (int)Layers.Ascended;
        }
        else gameObject.layer = (int)Layers.Player;
    }

    #endregion
}
