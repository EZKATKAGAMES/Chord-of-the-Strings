using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiantSun : MonoBehaviour
{
    public float radius;
    CombatConductor combatRef;
    // Use this for initialization
    void Start()
    {
        // In start becuase parent only exist once this object spawned (as child).
        combatRef = GetComponentInParent<CombatConductor>();
        combatRef.GetRadiantSunRef();

    }

    // Update is called once per frame
    void Update()
    {
        if (combatRef.a2_active == false)
            Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    // Function 1: damage

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.GetComponent<Enemy>())
        {
            // Apply status effect
        }
    }

    // Function 2: environment interaction

    // TODO: Cool shader stuff

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HiddenObject>())
        {
            HiddenObject hide = other.GetComponent<HiddenObject>();
            hide.colliders[0].enabled = true;
            
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<HiddenObject>())
        {
            HiddenObject hide = other.GetComponent<HiddenObject>();
            hide.colliders[0].enabled = false;
        }
        
    }


}
