using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suspendable : MonoBehaviour
{

    Rigidbody rigi;
    RigidbodyConstraints suspend;
    RigidbodyConstraints unsuspend;

    // Use this for initialization
    void Start()
    {
        rigi = GetComponent<Rigidbody>();
        // Stop all rotation and movement on rigidbodies
        suspend = RigidbodyConstraints.FreezeAll;
        unsuspend = RigidbodyConstraints.None;
    }

    // Update is called once per frame
    void Update()
    {
        /// TOOD: APPLY SUSPENDED EFFECT TO OBJECTS FOR A TIME LIMIT

    }

    void OnTriggerEnter(Collider other)
    {
        // If we collide with ability 3 hitbox.
        if (other.GetComponent<SpatialSuspension>())
        {
            // Suspend movement
            rigi.constraints = suspend;
            rigi.velocity = Vector3.zero;
        }
        else rigi.constraints = unsuspend;


    }
}
