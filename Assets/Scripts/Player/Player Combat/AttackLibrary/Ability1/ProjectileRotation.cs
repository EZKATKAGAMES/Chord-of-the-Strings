using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRotation : MonoBehaviour
{

    // Starshot Ref
    StarShot ability1Ref;

    public float rotationSpeed = 280;

    void Awake()
    {
        ability1Ref = GetComponentInParent<StarShot>();
    }

    void Update()
    {

        // Rotate clockwise.
        gameObject.transform.Rotate(0, 1, 0, Space.Self);
        // Model becomes more deplenished as charges are used.

        // Whenholding right click play animation, spawn big projectile.





    }
}
