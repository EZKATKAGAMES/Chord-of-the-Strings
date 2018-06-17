using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenObject : MonoBehaviour
{

    public Renderer rend;
    public Material[] mats;
    public BoxCollider[] colliders; // Collider 0 = the walkable collider | Collider 1 = trigger for detection.
    CombatConductor combatRef;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        colliders = GetComponents<BoxCollider>();
        colliders[0].isTrigger = false;
        colliders[1].isTrigger = true;

    }

    void Start()
    {
        combatRef = GameObject.FindGameObjectWithTag("Player").GetComponent<CombatConductor>();
    }

    // Update is called once per frame
    void Update()
    {
        // If ability has affected collider
        if (colliders[0].enabled == true)
        {
            // make it visible
            rend.material = mats[1];
        }
        else rend.material = mats[0];

        // If ability runs out or is cancelled.
        if(combatRef.a2_active == false)
        {
            // Revert to normal state
            rend.material = mats[0];
            colliders[0].enabled = false;
        }

    }

    
}
