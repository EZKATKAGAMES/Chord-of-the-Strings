using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    
    CapsuleCollider meleeColliderRef;
    public float timeTilNextMelee = 0.6f;
    public float timer1 = 0;
    public float timer2 = 0;
    public float activeTime = 0.3f;
    public float meleeLength = 1.3f;
    public bool readyToMelee = true;
    
    

    void Awake()
    {
        
    }
    
    void Start()
    {
        meleeColliderRef = GetComponentInChildren<CapsuleCollider>();
        meleeColliderRef.enabled = false;
    }

    
    void Update()
    {
        meleeColliderRef.height = meleeLength;
        MeleeAttack();

        
    }

    void MeleeAttack()
    {
        // Preparing to melee again
        if (meleeColliderRef.enabled == false && readyToMelee == false)
        {
            Debug.Log("Cooldown");         
            timer1 += Time.deltaTime;
            if(timer1 >= timeTilNextMelee)
            {
                readyToMelee = true;
                timer1 = 0;
            }
        }

        // Time window collider is active for.
        if(meleeColliderRef.enabled == true && readyToMelee == false)
        {
            timer2 += Time.deltaTime;
            if (timer2 >= activeTime)
            {
                meleeColliderRef.enabled = false; // Disable
                timer2 = 0;
            }
        }

        // Attacking
        if (readyToMelee == true && (Input.GetMouseButtonDown(0)))
        {
            meleeColliderRef.enabled = true; // Enable
            readyToMelee = false;



            Debug.Log("meleee");
        }

    }
        
            
        
}

  