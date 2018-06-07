using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(HorizontalVerticalVelocity))]
[RequireComponent(typeof(CombatConductor))]
public class PlayerCharacter : MonoBehaviour
{
    //Public Variables
    [Header("Movement")]
    public float moveSpeed = 0.0f;
    public float timeToJumpApex;
    public float maxJumpHeight;
    public bool grounded;

    [Header("Cursor Variables")]
    public Transform rotateTarget;
    public Vector3 mousePos;
    public float cameraRayLength = 0.0f;
    public float characterRotationAmount = 0.0f;
    public LayerMask collisionMask;
    public Info MouseVectorInfo;

    //Private Variables
    Rigidbody myRB;
    float jumpVelocity;
    float gravity;
    float moveHorizontal;
    float moveVertical;
    Vector3 velocity;
    Vector3 input;
    HorizontalVerticalVelocity axes;

    
    // TODO:

    void Start()
    {
        #region Setting up Variable References
        rotateTarget = GetComponentInChildren<Transform>();
        myRB = GetComponent<Rigidbody>();
        axes = GetComponent<HorizontalVerticalVelocity>();
        #endregion

        #region Jump Velocity Variables      
        gravity = (2 * maxJumpHeight / Mathf.Pow(timeToJumpApex,2)); // Set gravity;
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex; // Set jumpVelocity
        #endregion

        myRB.solverVelocityIterations = 30;
        myRB.freezeRotation = true;
    }

    private void Update()
    {
        ApplyGravity();
        #region PLAYER INPUT

        // Jump
        if (grounded && (Input.GetKeyDown(GameManager.GM.Jump)))
            Jump();
        // Movement Axes
        moveHorizontal = axes.horizontalVelocity;
        moveVertical = axes.verticalVelocity;

        #endregion

        

    }

    void FixedUpdate()
    {
        #region Movement
        input = new Vector3(moveVertical, 0, -moveHorizontal); // Movement stored into vector
        
        
        myRB.AddForce(input * moveSpeed, ForceMode.VelocityChange); // Apply the force to our rigidbody

        #endregion

        #region Mouse Aiming & LookDirection
        Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(raycast, out floorHit, cameraRayLength))
        {
            Vector3 mouseLocation = floorHit.point - transform.position;
            Quaternion aimRotation = Quaternion.LookRotation(mouseLocation);
            transform.rotation = Quaternion.Lerp(transform.rotation, aimRotation, Time.deltaTime * characterRotationAmount);
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        }
        #endregion

        

        #region Ground Check
        RaycastHit gc; // Hit
        if (Physics.Raycast(transform.position, Vector3.down, out gc, 0.6f, collisionMask)) // Ray
        {
            if (gc.collider.gameObject.layer == 9) // Int of layer (unity list): 9 is our collision layer           
                grounded = true;            
        }
        else grounded = false; // If our ray has no collision, we are not on standard ground.       
        Debug.DrawRay(transform.position, Vector3.down * 0.6f, Color.red); // Visual aid
        #endregion

        

    }
  
    void ApplyGravity() // Apply downward force when airborne
    {
        if(grounded == false)
        {
            velocity.y -= gravity * Time.deltaTime;
            myRB.AddForce(new Vector3(0, velocity.y), ForceMode.Acceleration);
        }
        else velocity.y = 0;  
    }

    void Jump() // Not handled through Fixed update, not sure if it will disrupt consistency?
    {
        myRB.velocity = new Vector3(0,jumpVelocity);
        //myRB.AddForce(0, jumpVelocity, 0, ForceMode.Impulse);
    }

    public struct Info
    {
        Vector3 mouseLocation;
        Quaternion aimRotation;
    }

}