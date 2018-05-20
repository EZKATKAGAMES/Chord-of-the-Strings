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
    
    float meleeAttackDistance = 1f;
    Vector2 currentDirectionX;

    // Range Attack Variables
    [Range(1, 4)]
    int selectedSpell = 1;
    
    GameObject ability1P1;

    // Cooldowns.


    void Start()
    {
        cursor = GetComponent<CursorMode>();
        rend = GetComponent<SpriteRenderer>();
        info = GetComponent<RaycastCharacterController>();

        ability1P1 = Resources.Load("Prefabs/PlayerAbilities/Ability1-P1") as GameObject;
    }



    // Update is called once per frame
    void Update()
    {
        currentDirectionX = new Vector2(info.colInfo.faceDirection, 0);

        

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
           // melee       
        }

        if (cursor.rangeMode && Input.GetMouseButtonDown(0))
        {
            
            Instantiate(ability1P1, cursor.projectileOrigin.transform.position, Quaternion.Euler(new Vector3(0, 0, cursor.projectileOrigin.transform.eulerAngles.z)));
        }
    }


}
