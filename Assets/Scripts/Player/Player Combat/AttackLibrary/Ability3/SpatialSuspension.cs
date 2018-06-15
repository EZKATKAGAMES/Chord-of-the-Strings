using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialSuspension : MonoBehaviour
{
    public float suspendedDuration;
    public float radius = 2f;
    CombatConductor combatRef;


    // Use this for initialization
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

        /// TOOD: APPLY SUSPENDED EFFECT TO OBJECTS FOR A TIME LIMIT

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
