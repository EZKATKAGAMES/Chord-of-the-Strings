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
    public int comboProgress;
    public bool readyToAttack = true;
    float resetCombo;
    public float continueCombo;
    Event mouseEvent;

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
        MeleeCombo1(1.2f, 1f, 0.7f);
        StartCoroutine(ReadyToAttack());
   
    }

    public void Melee()
    {
        meleeDirection = new Vector2(colInfo.colInfo.faceDirection, 0);     
        if(Input.GetMouseButton(0) && cursor.meleeMode && readyToAttack == true)
        {
            Debug.DrawRay(transform.position, meleeDirection * 2, Color.cyan);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, meleeDirection, meleeReach, enemy);
            if (hit.collider != null) // Hit something on enemy layer
            {
                readyToAttack = false;
                Debug.Log(hit.collider.name);
                if (hit.transform.GetComponent<Health>())
                {
                    ApplyDamage(hit);
                    
                }
            }
            else // hit nothing
            {               
                
            }

            

        }       
    }

    private void OnMouseDown()
    {
        if(readyToAttack == true)
        {
            Melee();
            comboProgress++;
            continueCombo = 0;
            MeleeComboMultipliers();
            readyToAttack = false;
        }

        

    }

    void ApplyDamage(RaycastHit2D hit)
    {
        Health test = hit.collider.GetComponent<Health>();
        test.TakeDamage(damage);
    }

    IEnumerator ReadyToAttack()
    {
        if(readyToAttack == false)
        {
            yield return new WaitForSeconds(0.15f);
            readyToAttack = true;
        }        
    }

    void MeleeCombo1(float comboReset1, float comboReset2, float comboReset3)
    {    
        if(comboProgress == 1)
        {
            
            continueCombo += (Time.deltaTime);
            if (continueCombo >= comboReset1) // When we reach our limit to reset combo
            {
                ComboReset();
            }
        }
        else if(comboProgress == 2)
        {       
            continueCombo += (Time.deltaTime);
            if (continueCombo >= comboReset2)
            {
                ComboReset();
            }           
        }
        else if(comboProgress == 3)
        {        
            continueCombo += (Time.deltaTime);
            if (continueCombo >= comboReset3)
            {
                ComboReset();
            }        
        }
        else if(comboProgress >= 4) // Stop over limit
        {
            comboProgress = 0;
            continueCombo = 0;
            damage = 20;
        }
    }

    void ComboReset()
    {
        readyToAttack = true;
        comboProgress = 0;
        continueCombo = 0;
    }

    void MeleeComboMultipliers()
    {
        if(comboProgress == 1)
        {
            damage = damage + 5;
        }
        else if(comboProgress == 2)
        {
            damage = damage + 10;
        }
        else if(comboProgress == 3)
        {
            damage = damage + 15;
            meleeReach = meleeReach * 2;
        }
        else if (comboProgress == 0)
        {
            meleeReach = 2;
            damage = 20;
        }
    }
   
}
