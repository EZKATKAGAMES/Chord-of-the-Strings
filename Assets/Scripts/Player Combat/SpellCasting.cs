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
       

        

        if (info.colInfo.faceDirection == -1)
        {
            rend.flipX = true;
        }
        else
        {
            rend.flipX = false;
        }

        if (cursor.meleeMode && Input.GetMouseButtonDown(0))
        {
            mAttack.Melee();
        }

        if (cursor.rangeMode && Input.GetMouseButtonDown(0))
        {
            
            Instantiate(rangedBasicAttack, cursor.projectileOrigin.transform.position, Quaternion.Euler(new Vector3(0, 0, cursor.projectileOrigin.transform.eulerAngles.z)));
        }
    }


}
