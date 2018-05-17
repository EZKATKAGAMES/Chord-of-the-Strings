using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script calculates player X & Y velocity, and handles jumping.

[RequireComponent(typeof(RaycastCharacterController))]
public class PlayerController : MonoBehaviour
{
    // Temporary public for testing values easily.

    // Movement
    public float maxJumpHeight = 10;
    public float minJumpHeight = 4;
    public float timeToJumpApex = 0.3f; // Time to reach jump height
    public float accelerationTimeAirborne = 0.2f; // Airborne smoothing
    public float accelerationTimeGrounded = 0.1f; // Grounded smoothing
    public float moveSpeed = 10;
    public float climbSpeedMax = 3;
    public bool climbing = false;
    

    public Vector2 wallJumpClimbingMovement;
    public Vector2 wallJumpClimbingStatic;
    public Vector2 wallLeap;

    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    float velocityXSmoothing;
    [HideInInspector]
    public Vector2 directionalInput;
    public Vector3 velocity;
    RaycastCharacterController controller;
   
    void Start()
    {
        #region Gravity & JumpVelocity Calculation
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2); // Set gravity
        maxJumpVelocity = Mathf.Abs(gravity) * Mathf.Pow(timeToJumpApex, 2); // Set jumpVelocity
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight); 
        print("Gravity:" + gravity + " Jump velocity: " + maxJumpVelocity + " MinJumpVelocity: " + minJumpVelocity); // Debug
        #endregion

        #region Reference Setup
        controller = GetComponent<RaycastCharacterController>();
        #endregion
    }

    void Update()
    {
        CalculateVelocity();
        ApplyGravity();

        if(directionalInput == Vector2.zero) // Stop velocity from gaining when we are not moving. (prevents odd bug when stood still next to a wall)
        {
            velocity.x = 0;
        }

        if (controller.colInfo.climb)
        {
            climbing = true;
        }
        else
        {
            climbing = false;
        }

        controller.Move(velocity * Time.deltaTime, directionalInput); // Apply movement

        #region Stop gravity accumulation
        
        // Stop gravity accumulation when on the floor.
        if (controller.colInfo.above || controller.colInfo.below)
        {
            velocity.y = 0;
        }

        // Stop gravity accumulating when climbing.
        if (climbing == true)
        {
            velocity.y = 0;
        }


        #endregion
    }    

    

    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }

    public void OnJumpInputDown()
    {
        if (climbing)
        {

            if (controller.colInfo.faceDirection == Mathf.Sign(directionalInput.x))
            {
                print("move");
                velocity.x = Mathf.Sign(controller.colInfo.faceDirection) * wallJumpClimbingMovement.x;
                velocity.y = wallJumpClimbingMovement.y;
                climbing = false;
            }
            else if (directionalInput.x == 0)
            {
                print("static");
                velocity.y = wallJumpClimbingStatic.y;
                climbing = false;
            }
            else if (directionalInput.y == 1) // maybe redundant, finish climb cooldown first.
            {
                print("leapUP");
                velocity.y = wallLeap.y;
                climbing = false;
            }
            else if( directionalInput.y == -1)
            {
                print("leapDOWN");
                velocity.y = -wallLeap.y;
                climbing = false;
            }
            
        }

        if (controller.colInfo.below)
        {
            velocity.y = maxJumpVelocity;
        }
    }

    public void OnJumpInputUp()
    {
        if (velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
        }       
    }

    void CalculateVelocity()
    {
        float targetVelocityX = directionalInput.x * moveSpeed; // Set target velocity

        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, // Apply smoothing amounts
        (controller.colInfo.below) ? accelerationTimeGrounded : accelerationTimeAirborne); // Depending if we are airborne or not
        

    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime; // Apply gravity
    }

    
}
