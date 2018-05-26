using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCasting : MonoBehaviour
{
    // This script will be responsible for executing code to cast spells & the animations that belong to them.

    // Reference Variables
    CursorMode cursor;
    RaycastCharacterController info;
    SpriteRenderer rend;

    // Melee Attack Variables
    MeleeBasicAttack mAttack;
    
    // Range Attack Variables
    [Range(1, 4)]
    int selectedSpell = 1;

    
    GameObject rangedBasicAttack;
    GameObject ability1;

    


    #region Ability1 Variables
    [Header("Ability1 Values")]
    public float a1_Damage = 20;
    public float a1_Speed = 5;
    public float a1_Charging;
    float a1_ChargeLimit = 5f;
    public float a1_ForceTimer = 0;
    float a1_ForceFireLimit = 3f;
    public bool onCD;
    public float cdTimer;
    int coolDown = 5;
    #endregion

    void Start()
    {
        cursor = GetComponent<CursorMode>();
        rend = GetComponent<SpriteRenderer>();
        info = GetComponent<RaycastCharacterController>();
        mAttack = GetComponent<MeleeBasicAttack>();

        rangedBasicAttack = Resources.Load("Prefabs/PlayerAbilities/RangedBasicAttack") as GameObject;
        ability1 = Resources.Load("Prefabs/PlayerAbilities/Ability1") as GameObject;
    }



    // Update is called once per frame
    void Update()
    {
        MeleeAttack();
        RangedAttack();
        FlipSprite();
        #region Ability1 Functions
        ChargeSpellAbility1();
        a1_CDTimer(coolDown);
        #endregion
    }

    #region FlipSprite, Melee & Ranged AA
    void FlipSprite()
    {
        if (info.colInfo.faceDirection == -1)
        {
            rend.flipX = true;
        }
        else
        {
            rend.flipX = false;
        }
    }

    void RangedAttack()
    {
        if (cursor.rangeMode && Input.GetMouseButtonDown(0))
        {
            Instantiate(rangedBasicAttack, cursor.projectileOrigin.transform.position, Quaternion.Euler(new Vector3(0, 0, cursor.projectileOrigin.transform.eulerAngles.z)));
        }
    }
    void MeleeAttack()
    {
        if (cursor.meleeMode && Input.GetMouseButtonDown(0))
        {
            mAttack.Melee();
            
        }
    }
    #endregion

    #region Ability1Functions
    void ChargeSpellAbility1()
    {
        // TODD: Apply root to player while charging.

        a1_CalculateMultipliers();

        if (Input.GetKey(GameManager.GM.Ab1) && onCD == false)
        {
            a1_Charging += Time.deltaTime; // Charing while we are holding ability1 key
            if (a1_Charging > a1_ChargeLimit)
            {
                a1_Charging = a1_ChargeLimit; // Cap so that we do not go over max charge.

                a1_ForceTimer += Time.deltaTime; // Start timer to force spell cast
                if (a1_ForceTimer >= a1_ForceFireLimit)
                {
                    a1_ForceTimer = 0;
                    a1_Fire(); // Force fire
                    a1_AbilityCoolDown(); // Start cd
                    ResetValuesA1();
                }
            }
        }

        // Fire the spell if we release the key.
        if (a1_Charging > 0 && Input.GetKeyUp(GameManager.GM.Ab1) || a1_ForceTimer == a1_ForceFireLimit)
        {
            a1_Fire();
            a1_AbilityCoolDown(); // Start cd
            ResetValuesA1();
        }
    }
    void ResetValuesA1()
    {     
        a1_Charging = 0;
        a1_Speed = 5;
        a1_Damage = 20;
    }
    void a1_CalculateMultipliers()
    {
        if(a1_Charging > 1 && a1_Charging < 2)
        {
            a1_Damage += 0.1f;
            a1_Speed += 0.1f;
            
        }
        else if (a1_Charging > 2 && a1_Charging < 3)
        {
            a1_Damage += 0.2f;
            a1_Speed += 0.2f;
        }
        else if (a1_Charging > 3 && a1_Charging < 4)
        {
            a1_Damage += 0.3f;
            a1_Speed += 0.3f;
        }
        else if(a1_Charging > 4 && a1_Charging < 5)
        {
            a1_Damage += 0.3f;
            a1_Speed += 0.3f;
            if(a1_Charging == 5)
            {
                return;
            }
        }
        
    }
    void a1_Fire()
    {
        Instantiate(ability1, cursor.projectileOrigin.transform.position, Quaternion.Euler(new Vector3(0, 0, cursor.projectileOrigin.transform.eulerAngles.z)));
    }
    void a1_AbilityCoolDown()
    {
        onCD = true;   
    }

    void a1_CDTimer(float cd)
    {
        if(onCD == true)
        {
            cdTimer += Time.deltaTime;
            if (cdTimer >= cd)
            {
                onCD = false;
            }

            if (Input.GetKeyDown(GameManager.GM.Ab1))
            {
                print("Cooldown: " + cdTimer.ToString());
            }
        }
        else
        {
            cdTimer = 0;
        }
       
    }
    #endregion

}
