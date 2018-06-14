using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRotation : MonoBehaviour
{
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
        // Change animation/model/texture depending on starshot charges

    }
}
