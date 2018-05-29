using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    //Public Variables
    public float moveSpeed = 0.0f;
    public Transform rotateTarget;
    public Vector3 mousePos;
    public float cameraRayLength = 0.0f;
    public float characterRotationAmount = 0.0f;
    //Private Variables
    private Rigidbody myRB;

    void Start ()
    {
        rotateTarget = GetComponentInChildren<Transform>();
        myRB = GetComponent<Rigidbody>();
    }
	
	void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(moveHorizontal, 0, moveVertical);

		if(Input.GetAxis("Horizontal") != 0|| (Input.GetAxis("Vertical") != 0))
        {
            print("TestMovement");
            myRB.AddForce(movementDirection * moveSpeed, ForceMode.VelocityChange);
        }

        Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(raycast, out floorHit, cameraRayLength))
        {
            Vector3 mouseLocation = floorHit.point - transform.position;
            Quaternion aimRotation = Quaternion.LookRotation(mouseLocation);
            transform.rotation = Quaternion.Lerp(transform.rotation, aimRotation, Time.deltaTime * characterRotationAmount);
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        }

    }

    private void Update()
    {
        
    }
}
