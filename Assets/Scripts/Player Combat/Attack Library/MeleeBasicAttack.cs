using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBasicAttack : MonoBehaviour
{

    CursorMode cursor;
    RaycastCharacterController colInfo;
    Vector2 meleeDirection;
    public int damage;
    public float meleeReach;
    public LayerMask enemy;
    public int comboCount;
    public bool readyToAttack = true;
    float resetCombo;
    public float continueCombo;

    bool ability4Override;

    // Use this for initialization
    void Start()
    {
        colInfo = GetComponent<RaycastCharacterController>();
        cursor = GetComponent<CursorMode>();
        enemy = 1 << 11; // BitShift (2048)
        
    }

    private void Update()
    {
        MeleeCombo1(1f, 0.7f, 0.4f);
        MeleeComboMultipliers();
    }

    public void Melee()
    {
        meleeDirection = new Vector2(colInfo.colInfo.faceDirection, 0);
     
        if (Input.GetMouseButton(0) && cursor.meleeMode && readyToAttack == true && ability4Override == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, meleeDirection, meleeReach, enemy);
            if (hit.collider != null)
            {
                readyToAttack = false;
                Debug.Log(hit.collider.name);
                if (hit.transform.GetComponent<Health>())
                {
                    ApplyDamage(hit);
                    comboCount++;
                    readyToAttack = false;
                }
            }
            else
            {
                // Addcombocount
                readyToAttack = false;
                
            }
        }

        
    }

    void AddComboCount()
    {
        if (Input.GetMouseButton(0))
        {
            comboCount++;
        }
    }

    void ApplyDamage(RaycastHit2D hit)
    {
        Health test = hit.collider.GetComponent<Health>();
        test.TakeDamage(damage);
    }

    void MeleeCombo1(float comboReset1, float comboReset2, float comboReset3)
    {
       

        if(comboCount >= 1)
        {

            continueCombo += (Time.deltaTime);
            if (continueCombo >= comboReset1) // When we reach our limit to reset combo
            {               
                readyToAttack = true;
                comboCount = 0;
                continueCombo = 0;
            }
            else // When we are within the limit to extend combo
            {
                comboCount++;
            }

        }
        else if(comboCount >= 2)
        {
            continueCombo = 0;
            continueCombo += (Time.deltaTime);
            if (continueCombo >= comboReset2)
            {
                readyToAttack = true;
                comboCount = 0;
                continueCombo = 0;
            }
            else
            {
                comboCount++;
            }
        }
        else if(comboCount >= 3)
        {
            continueCombo += (Time.deltaTime);
            if (continueCombo >= comboReset3)
            {
                readyToAttack = true;
                comboCount = 0;
                continueCombo = 0;
            }
            else
            {
                comboCount++;
            }
        }
    }

    void MeleeComboMultipliers() // Use enum instead!!!
    {
        if(comboCount == 1)
        {
            damage *= (int)0.5f;
        }
        else if(comboCount == 2)
        {
            damage *= 1;
        }
        else if(comboCount == 3)
        {
            damage *= 2;
        }
        else
        {
            damage = 20;
        }
    }
   
}
