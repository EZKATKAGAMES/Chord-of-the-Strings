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

    // Cooldowns.


    #region Ability1 Variables
    [Header("Ability1 Values")]
    public float a1_Damage = 20;
    public float a1_Speed = 5;
    public float a1_Charging;
    float a1_ChargeLimit = 5f;
    public float a1_ForceTimer = 0;
    float a1_ForceFireLimit = 3f;

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
        ChargeSpellAbility1();

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

    void ChargeSpellAbility1()
    {
        a1_CalculateMultipliers();

        if (Input.GetKey(GameManager.GM.Ab1))
        {
            a1_Charging += Time.deltaTime;
            if (a1_Charging > a1_ChargeLimit)
            {
                a1_Charging = a1_ChargeLimit;

                a1_ForceTimer += Time.deltaTime;
                if (a1_ForceTimer >= a1_ForceFireLimit)
                {
                    a1_ForceTimer = 0;
                    a1_Fire();
                    // Start cooldown
                    StartCoroutine(ResetValuesA1());
                }
            }
        }

        // apply damage and force multipliers, fire the spell with those values and reset them.
        if (a1_Charging > 0 && Input.GetKeyUp(GameManager.GM.Ab1) || a1_ForceTimer == a1_ForceFireLimit)
        {
            
            a1_Fire();
            // Start cooldown
            StartCoroutine(ResetValuesA1());
        }
    }

    IEnumerator ResetValuesA1()
    {
        yield return new WaitForSeconds(0.1f);
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

}
