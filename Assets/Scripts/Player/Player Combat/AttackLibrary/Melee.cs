using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    //TODO: Match time collider is active to animation.
   
    [Header("Timers")]
    public float timer1 = 0; // Counts up towards timeNextMelee to ready attacks.
    public float timeTilNextMelee = 0.6f; // Time before attacking is possible again.
    [Header("      ")]  
    public float timer2 = 0; // Counts up towards activeTime, to deactivate collider.
    public float activeTimeLimit = 0.3f; // Time collider is active for.
    [Header("      ")]
    public float timer3 = 0; // Counts up towards comboResetTimer, to reset combo.
    public float comboResetLimit = 2; // Resets combo when no input is recieved before this number reaches 0.
    [HideInInspector]
    public int comboProgress = 0; // Moves progress of melee combo.
    [Header("Properties")]
    public float meleeLength = 1.3f; // Length of the collider.
    public bool readyToMelee = true;
    public bool enemyHit = false;

    // Private
    CombatConductor combatRef;
    CapsuleCollider meleeColliderRef;

    void Start()
    {
        meleeColliderRef = GetComponentInChildren<CapsuleCollider>();
        meleeColliderRef.enabled = false;
        combatRef = GetComponentInParent<CombatConductor>();
    }

    void Update()
    {
        meleeColliderRef.height = meleeLength;
        MeleeComboProgression(); 
    }

    #region Core functionality
    public void MeleeAttack()
    {
        // Preparing to melee again
        if (meleeColliderRef.enabled == false && readyToMelee == false)
        {
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
            if (timer2 >= activeTimeLimit)
            {
                meleeColliderRef.enabled = false; // Disable
                timer2 = 0;
            }
        }

        // Attacking
        if (readyToMelee == true && (Input.GetMouseButtonDown(0)))
        {
            meleeColliderRef.enabled = true; // Enable
            comboProgress++; // Increase our combo
            timer3 = 0; // Reset our window to perform combo
            readyToMelee = false;
        }

        if (meleeColliderRef.enabled == false)
            enemyHit = false;

    }
    #endregion

    // Knockback upon hit detection
    private void OnTriggerEnter(Collider other)
    {
        // Enemies that derive from enemy base class
        if (other.GetComponent<Enemy>())
        { 
            enemyHit = true;

            if(enemyHit == true)
            {
                Debug.Log("hitenemy");
                // Apply knockback when hitting   
                Rigidbody knock = other.GetComponent<Rigidbody>();           
                Vector3 force = (other.transform.position - transform.position).normalized;
                knock.AddRelativeForce(force * combatRef.knockForce, ForceMode.Impulse);
                return;
            }
            
        }
        
    }

    #region Combo function
    void MeleeComboProgression() 
    {
        if(comboProgress == 1) // ComboProgress 1
        {
            
            timer3 +=  (Time.deltaTime);
            if (timer3 >= comboResetLimit)
            {
                timer3 = 0;
                comboProgress = 0; // Reset progress if we do not melee in time.
            }
        }
        else if(comboProgress == 2) // ComboProgress 2
        {
            
            timer3 += Time.deltaTime;
            if (timer3 >= comboResetLimit)
            {
                timer3 = 0;
                comboProgress = 0; // Reset progress if we do not melee in time.
            }
        }
        else if(comboProgress == 3) // ComboProgress 3
        {
           
            timer3 += (Time.deltaTime);
            if (timer3 >= comboResetLimit)
            {
                timer3 = 0;
                comboProgress = 0; // Reset progress if we do not melee in time.
            }
        }
        else if(comboProgress >= 4) // This is actually 5th click
        {
            comboProgress = 0;
            timer3 = 0;
        }



    }
    #endregion
}