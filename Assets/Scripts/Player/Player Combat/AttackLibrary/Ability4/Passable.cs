using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(BoxCollider))]
public class Passable : MonoBehaviour
{
    BoxCollider[] colliders;
    Renderer rend;
    public Material[] mats; // 0 default, 1 new
    CombatConductor combatRef;

    void Start()
    {
        combatRef = GameObject.FindGameObjectWithTag("Player").GetComponent<CombatConductor>();
        colliders = GetComponents<BoxCollider>();
        rend = GetComponent<Renderer>();

        gameObject.tag = "Passable";
        colliders[0].isTrigger = false;
        colliders[1].isTrigger = true;
        
    }

    
    void Update()
    {

    }
}
