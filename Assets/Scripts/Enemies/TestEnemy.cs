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



    private void OnTriggerEnter(Collider other)
    {
        // Take damage
        if(other.gameObject.layer == 11)
        {
            Debug.Log("OW!");
            CombatConductor mDmg = other.GetComponentInParent<CombatConductor>();
            TakeDamage((int)mDmg.meleeDamage);
        }

        // Knockback if player collides
        if (other.gameObject.tag == "Player")
        {

            Rigidbody knock = other.GetComponent<Rigidbody>();
            Vector3 force = (other.transform.position - transform.position).normalized;
            knock.AddRelativeForce(force * -knockForce, ForceMode.Impulse);
        }
    }

  
}
