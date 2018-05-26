using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability2 : MonoBehaviour
{
    // Create a slow moving projectile that hovers in place for a short moment
    // The player can shoot projectiles into this and then once it has entered they will be allowed to refire that projectile from the prespective of the orb
    // ?? Maybe enemy projectiles will be stored until the duration of the orb has expired. They will be fired out randomly.

    SpellCasting reference;
    public float projectileSpeed;
    public float lifeSpan = 5f;
    public float damage;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
