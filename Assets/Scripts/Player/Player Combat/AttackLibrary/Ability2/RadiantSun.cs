using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiantSun : MonoBehaviour
{
    public float radius;
    CombatConductor combatRef;

    void Start()
    {
        // In start becuase parent only exist once this object spawned (as child).
        combatRef = GetComponentInParent<CombatConductor>();
        combatRef.GetRadiantSunRef();
    }
    
    void Update()
    {
        if (combatRef.a2_active == false)
            Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    // Make objects interactable
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HiddenObject>())
        {
            HiddenObject hide = other.GetComponent<HiddenObject>();
            hide.colliders[0].enabled = true;        
        } 
    }
    // Return objects to normal
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<HiddenObject>())
        {
            HiddenObject hide = other.GetComponent<HiddenObject>();
            hide.colliders[0].enabled = false;
        }      
    }
}
