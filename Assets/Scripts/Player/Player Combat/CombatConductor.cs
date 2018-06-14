using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatConductor : MonoBehaviour
{
    PlayerCharacter mouseInfoRef;

    [Range(1, 4)]
    public int selectedSpell = 1;

    #region Melee Variables
    public float meleeDamage;
    public float knockForce;
    Melee meleeRef;
    #endregion

    #region Ability1: StarShot Variables
    [Header("Ability1 Variables")]
    public GameObject ability1;
    public StarShot starShotRef;
    public bool spellActive;
    public bool readyToActivate = true; // Cooldown for ability
    public bool readyToFire = false; // Cooldown for firing projectiles
    public bool chargeReady;
    [Header("   ")] // Inspector aesthetics!!!
    public float timer1 = 0; // Counts towards the cooldown limit
    public float ability1CooldownLimit = 0.6f; // Threshold we must reach before we can cast the spell again
    [Header("   ")] // Inspector aesthetics!!!
    public float timer2 = 0; // Counts towards the fire delay
    public float delayBetweenFire = 0.3f; // The time we must wait before firing another shot.
    [Header("   ")] // Inspector aesthetics!!!
    public float timer3 = 0; // Charge this up to cast charged ability1
    public float chargeLimit = 3.0f;



    #endregion

    #region Ability2: RadiantSun Variables
    [Header("Ability1 Variables")]
    public float meme;
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
            readyToFire = false;
            spellActive = false;
            timer2 = 0;
            timer3 = 0;
        }

        // Ability Functionality
        if (selectedSpell == 1)
        {
           // If we press rightmouse button once we have the spell selected.
           if (Input.GetMouseButtonDown(1) && readyToActivate == true)
           {
                // Activate the spell
                spellActive = true;
                Instantiate(ability1, gameObject.transform);
                StartCoroutine(DelayAfterActivation());
                readyToActivate = false;
           }
           // Charging
           if (starShotRef != null)
           {
               if (Input.GetMouseButton(1) && starShotRef.charges > 0 && spellActive)
                   timer3 += Time.deltaTime;
               else if (Input.GetMouseButtonUp(1)) // Reset on button release
                   timer3 = 0;
                // Allow charge attacks
                if (timer3 >= chargeLimit)
                {
                    chargeReady = true;
                    timer3 = 0;
                }
            }           
           // If we release fire button and charge timer exceeds the limit, do a charge attack
           if (readyToFire == true && spellActive == true && chargeReady == true && starShotRef.charges > 0 && Input.GetMouseButtonUp(1))
           {
                starShotRef.HoldFire();
                timer3 = 0;   
           }
           // if not then do a regular attack
           if(readyToFire == true && spellActive == true && chargeReady == false && starShotRef.charges > 0 && Input.GetMouseButtonUp(1))
           {   
                starShotRef.PressFire();
                timer3 = 0;
           }         
        }
        #region Cooldowns
        
        // Ability CD
        if(readyToActivate == false && spellActive == false)
        {
            timer1 += Time.deltaTime;
            if(timer1 >= ability1CooldownLimit)
            {
                readyToActivate = true;
                timer1 = 0;
            }
        }
        // Reset timer 2 
        if (readyToFire == true)
            timer2 = 0;
        // After the initial ready to fire
        if (readyToFire == false && spellActive == true)
        {
            timer2 += Time.deltaTime;
            if (timer2 >= delayBetweenFire)
            {
                readyToFire = true;
                timer2 = 0;
            }
        }
        #endregion
    }

    // Initial ready to fire
    IEnumerator DelayAfterActivation()
    {
        yield return new WaitForSeconds(0.2f);
        readyToFire = true;
    }

    public void GetStarShotRef()
    {
        // Reference the newly spawned prefab, which is a child object to us.
        starShotRef = gameObject.GetComponentInChildren<StarShot>();
        Debug.Log(starShotRef.name);
    }

    #endregion


    #region Ability2



    #endregion

}
