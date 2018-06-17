using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ascension : MonoBehaviour
{
    PlayerCharacter moveReference;
    CombatConductor combatRef;
    GameObject fX;
    public Renderer rend; // Player renderer


    // Use this for initialization
    void Start()
    {
        // Player Renderer
        rend = GetComponentInParent<Renderer>();
        moveReference = GetComponentInParent<PlayerCharacter>();
        combatRef = GetComponentInParent<CombatConductor>();
        combatRef.GetAscensionRef();

        if (combatRef.a4_active)
        {
            float speed = moveReference.moveSpeed + 1;
            moveReference.moveSpeed = speed;
        }

    }

    // Update is called once per frame
    void Update()
    {

        // Apply effects when active
        if(combatRef.a4_active == true)
        {
            rend.material = moveReference.mats[1];
        }
        else
        {
            rend.material = moveReference.mats[0];
            moveReference.moveSpeed = PlayerCharacter.initialSpeed; 
            Destroy(gameObject);
        }
    }
}
