using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testenemyAI : MonoBehaviour
{
    public enum State {Patrol, Aggro, Attack, BattleMechanic1};
    State behaviourState;
    public float aggroRadius; // The radius of when the enemy can begin to aggro onto the player.
    public float maxJumpHeight = 10;
    public float minJumpHeight = 4;
    public float timeToJumpApex = 0.3f; // Time to reach jump height
    public float accelerationTimeAirborne = 0.2f; // Airborne smoothing
    public float accelerationTimeGrounded = 0.1f; // Grounded smoothing
    public float moveSpeed = 10;
    public float climbSpeedMax = 3;
    public bool climbing = false;

    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    float velocityXSmoothing;

    [HideInInspector]
    public Vector2 directionalInput;
    public Vector3 velocity;
    RaycastCharacterController controller;
    // Use this for initialization
    void Start()
    {
        #region Gravity & JumpVelocity Calculation
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2); // Set gravity
        maxJumpVelocity = Mathf.Abs(gravity) * Mathf.Pow(timeToJumpApex, 2); // Set jumpVelocity
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        print("Gravity:" + gravity + " Jump velocity: " + maxJumpVelocity + " MinJumpVelocity: " + minJumpVelocity); // Debug
        #endregion

    }

    // Update is called once per frame
    void Update()
    {
        CalculateVelocity();
        ApplyGravity();
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
