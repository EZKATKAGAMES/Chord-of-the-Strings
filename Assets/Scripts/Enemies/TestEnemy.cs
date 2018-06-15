using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemy : Enemy
{
    public NavMeshAgent agent;
    public Transform target;
    public Rigidbody rigi;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rigi = GetComponent<Rigidbody>();
        target = GameObject.Find("TestPlayer11(Clone)").transform;
    }
   
    void Update()
    {
        agent.speed = movementSpeed;
        // Braindead seek for testing
        agent.SetDestination(target.position);

        Death();
    }


    // DETECTION BUG: CAUSE OF OVERDAMAGE AND OVERKNOCKBACK 

    private void OnTriggerEnter(Collider other)
    {
        // Take damage, Melee
        if(other.gameObject.layer == 11)
        {
            Debug.Log("OW!");
            CombatConductor mDmg = other.GetComponentInParent<CombatConductor>();
            TakeDamage(mDmg.meleeDamage);
        }
        // Take damage, projectiles
        if(other.gameObject.layer == 12)
        {
            Debug.Log("OW!");
            ProjectileTranslation a1Dmg = other.GetComponentInParent<ProjectileTranslation>();
            TakeDamage(a1Dmg.a1Damage);
        }  

        // Knockback if player collides
        if (other.gameObject.tag == "Player")
        {

            Rigidbody knock = other.GetComponent<Rigidbody>();
            Vector3 force = (other.transform.position - transform.position).normalized;
            knock.AddRelativeForce(force * -knockForce, ForceMode.Impulse);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Take damage, ability2
        if (other.GetComponent<RadiantSun>())
        {
            float delta = Time.deltaTime;
            CombatConductor a2Dmg = other.GetComponentInParent<CombatConductor>();
            TakeDamage(a2Dmg.a2_damage * delta);
        }
    }


}
