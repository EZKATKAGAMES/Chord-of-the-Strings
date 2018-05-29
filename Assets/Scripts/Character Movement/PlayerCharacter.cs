using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    //Public Variables
    [Header("Movement")]
    public float moveSpeed = 0.0f;
    public float timeToJumpApex;
    public float maxJumpHeight;

    public Transform rotateTarget;
    public Vector3 mousePos;
    public float cameraRayLength = 0.0f;
    public float characterRotationAmount = 0.0f;

    //Private Variables
    private Rigidbody myRB;
    private float jumpVelocity;
    public float gravity;
    public Vector2 velocity;
    Vector3 input;

    void Start()
    {
        #region Setting up variables
        rotateTarget = GetComponentInChildren<Transform>();
        myRB = GetComponent<Rigidbody>();
        #endregion

        #region Jump Velocity Variables
        velocity = myRB.velocity; // Rigid body current velocity
        gravity = (2 * maxJumpHeight / Mathf.Pow(timeToJumpApex,2)); // Set gravity;
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex; // Set jumpVelocity
        #endregion

    }

    private void Update()
    {
        ApplyGravity();
    }

    void FixedUpdate()
    {
        #region Movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        input = new Vector3(moveHorizontal, 0, moveVertical);

        if (Input.GetAxis("Horizontal") != 0 || (Input.GetAxis("Vertical") != 0))
        {
            print("TestMovement");
            myRB.AddForce(input * moveSpeed, ForceMode.VelocityChange);
        }

        

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

        


    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.collider.gameObject.layer == 9) // temporary ground check do proper later
        {
            velocity.y = 0; // Prevent gravity accumulations MOVE THIS TO PHYSICS.cast CHECK

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;

        
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpVelocity;
            myRB.AddForce(new Vector3(0,velocity.y), ForceMode.Impulse); // Apply the velocity values to our rigid body as an impulse force.

        }

       
    }
}