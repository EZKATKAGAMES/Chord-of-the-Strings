using System.Collections;
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
    public GameObject ability1;

    public bool spellActive;

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

    void Ability1Handling()
    {
        // If we have starshot selected.
        if(selectedSpell == 1)
        {
            if(spellActive == false && Input.GetMouseButtonDown(1))
            {
                Instantiate(ability1, gameObject.transform);
                // Start Cooldown
                spellActive = true;
            }
        }



    }

   


}
