using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class ProjectileRotation : MonoBehaviour // Data
{
    // Centroid of triangle
    public Transform rotationPoint;
    
    public int projectileCount = 0;

    [HideInInspector]
    public float rotationSpeed = 280;

    public bool inTransit;

    #region Speed Variables
    [Tooltip("The speed projectiles rotate around centriod, getting faster when less projectiles are available")]
    public float speed3 = 90;
    [Tooltip("The speed projectiles rotate around centriod, getting faster when less projectiles are available")]
    public float speed2 = 180;
    [Tooltip("The speed projectiles rotate around centriod, getting faster when less projectiles are available")]
    public float speed1 = 270;
    #endregion

}
class RotationSystem : ComponentSystem // Behaviour
{
    public struct Components
    {
        public ProjectileRotation rotation; // Access our variables
        public Transform transform;
        public StarShot projectileProperties;
    }

    protected override void OnStartRunning()
    {
       
        
    }

    protected override void OnUpdate()
    {
        float delta = Time.deltaTime;

        
        foreach (var ent in GetEntities<Components>())
        {
            // Rotate around the centriod while the projectile is not being fired
            if (!ent.projectileProperties.inTransit)
            {
                ent.transform.RotateAround(ent.rotation.rotationPoint.position, new Vector3(0, 1, 0), ent.rotation.rotationSpeed * delta);
            }
           

            ent.rotation.projectileCount = GetEntities<Components>().Length; // Projectile count is equal to the amount of projectile entities.

            // Set speed based on projectile count.
            #region rotationSpeed
            if (ent.rotation.projectileCount == 3)
            {
                ent.rotation.rotationSpeed = ent.rotation.speed3;
            }
            else if(ent.rotation.projectileCount == 2)
            {
                ent.rotation.rotationSpeed = ent.rotation.speed2;
            }
            else if(ent.rotation.projectileCount == 1)
            {
                ent.rotation.rotationSpeed = ent.rotation.speed3;
            }
            #endregion

        }

    }

}
