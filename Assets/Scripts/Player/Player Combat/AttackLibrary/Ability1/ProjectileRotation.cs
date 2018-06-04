using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRotation : MonoBehaviour
{
    public Transform target;

    public float rotationSpeed;
    void Update()
    {
        transform.RotateAround(target.position,new Vector3(0,1,0), rotationSpeed * Time.deltaTime);
    }
}
