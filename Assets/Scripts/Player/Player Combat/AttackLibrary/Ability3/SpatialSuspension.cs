using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialSuspension : MonoBehaviour
{
    public float suspendedDuration;
    public float radius = 2f;
    CombatConductor combatRef;

    void Start()
    {
        combatRef = GetComponentInParent<CombatConductor>();
        combatRef.GetSpacialSuspensionRef();
    }

    // Update is called once per frame
    void Update()
    {
        if (combatRef.a3_active == false)
            Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If we detect suspendable object
        if (other.GetComponent<Suspendable>())
        {
            Suspendable sus = other.GetComponent<Suspendable>();
            sus.Locked();
        } 
    }

}
