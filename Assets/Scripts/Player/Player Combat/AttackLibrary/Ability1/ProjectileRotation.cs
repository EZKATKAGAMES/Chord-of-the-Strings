using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRotation : MonoBehaviour
{

    // Starshot Ref
    StarShot ability1Ref;

    // Centroid of triangle
    public Transform rotationPoint;

    
    public int projectileCount = 0;

    [HideInInspector]
    public float rotationSpeed = 280;

    

    #region Speed Variables
    [Tooltip("The speed projectiles rotate around centriod, getting faster when less projectiles are available")]
    public float speed3 = 90;
    [Tooltip("The speed projectiles rotate around centriod, getting faster when less projectiles are available")]
    public float speed2 = 180;
    [Tooltip("The speed projectiles rotate around centriod, getting faster when less projectiles are available")]
    public float speed1 = 270;
    #endregion

    void Awake()
    {
        ability1Ref = GetComponentInParent<StarShot>();
    }

    void Update()
    {
        // Rotate around the centriod, on the Y axis.
        gameObject.transform.RotateAround(rotationPoint.position, new Vector3(0, 1, 0), rotationSpeed * Time.deltaTime);

        // Number of active projectiles. This determines the rotation speed
        projectileCount = ability1Ref.projectiles;
        
        #region rotationSpeed
        if (projectileCount == 3)
        {
            rotationSpeed = speed3;
        }
        else if (projectileCount == 2)
        {
            rotationSpeed = speed2;
        }
        else if (projectileCount == 1)
        {
            rotationSpeed = speed1;
        }
        #endregion

        Vector3 meme = transform.position - rotationPoint.position;

        Debug.DrawRay(transform.position, -meme, Color.red);
    }
}
