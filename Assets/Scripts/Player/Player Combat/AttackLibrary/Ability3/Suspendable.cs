using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Suspendable : MonoBehaviour
{
    Rigidbody rigi;
    public bool locked;
    public float suspensionTime;
    public float suspensionLimit;
    //Objects
    RigidbodyConstraints suspend;
    RigidbodyConstraints unsuspend;
    //Enemies
    NavMeshAgent agent;

    void Start()
    {
        rigi = GetComponent<Rigidbody>();
        if (gameObject.tag == "Enemy")
        {
            agent = GetComponent<NavMeshAgent>();
        }
        else agent = null;
        // Stop all rotation and movement on rigidbodies
        suspend = RigidbodyConstraints.FreezeAll;
        unsuspend = RigidbodyConstraints.None;   
    }

    void Update()
    {      
        Duration();
    }

    public void Locked()
    {
        Debug.Log("I haveu StOPPED moVING");
        // Rigi
        rigi.constraints = suspend;
        rigi.velocity = Vector3.zero;
        locked = true;

        if (agent != null)
        {
            // Nav agent
            agent.isStopped = true;
            locked = true;
        }
    }

    // Resume movement/unlock after timer is over.
    void Duration()
    {
        if (locked)
        {
            suspensionTime += Time.deltaTime;
            if (suspensionTime >= suspensionLimit)
            {
                locked = false;
                rigi.constraints = unsuspend;
                suspensionTime = 0;
                if(agent != null)
                {
                    agent.isStopped = false;
                }              
            }
        }
    }  
}
