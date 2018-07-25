using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrthograpicCameraFollow : MonoBehaviour
{
    // GET TARGET
    // APPLY SMALL AMOUNT OF SMOOTHING

    public GameObject camera;
    public Transform target;
    public float smoothingAmount;
    public float cameraZoom = 18; // Zoom = Gameobject y position
    public bool cameraRotateN; // Negative
    public bool cameraRotateP; // Positive
    public bool cameraRotating; // When the camera is in rotation
    public bool view1 = true; //45
    public bool view2; //-45
    public bool view3; //-140
    public bool view4; // 140
    public float rotationY = 45; // Default View
    public float rotationStrength = 5;
    public float rotationSpeed = 0.3f;

    public Vector3 defaultPosition;
    public Vector3 defaultRotation;

    public float lerpTargetPositive = 0;
    public float lerpTargetNegative = 0;
    

    private void Awake()
    {
        camera = gameObject;
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        // Set defaults
        defaultPosition = new Vector3(0, 18, 0);
        defaultRotation = new Vector3(30, 45, 0);
        // Apply them at start.
        gameObject.transform.position = defaultPosition;
        gameObject.transform.rotation = Quaternion.Euler(defaultRotation);
    }

  
    void Update()
    {
        RotateCamera();


        FollowTarget();
    }

  
    void FollowTarget()
    {
        // Target x, camerazoom Y, target Z
        gameObject.transform.position = new Vector3(target.transform.position.x, cameraZoom, target.transform.position.z);
    }

    void RotateCamera()
    {

        // Allow rotation when the camera is not currently rotating
        if (Input.GetKeyDown(KeyCode.E) && !cameraRotating)
        {
            rotationStrength += Time.deltaTime * rotationSpeed;
            rotationY = Mathf.Lerp(rotationY, lerpTargetPositive, rotationStrength);

        }

        

      

        if (Input.GetKeyDown(KeyCode.Q) && !cameraRotating)
        {
            // Increase rotation speed as it rotates.
            rotationStrength += Time.deltaTime * rotationSpeed;
            rotationY = Mathf.Lerp(rotationY, lerpTargetPositive, rotationStrength);
        }

        if (cameraRotateP)
        {
           

           
        }

        // Apply rotation
        // X is being set to static default value. Y is set to dynamic value we change.
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(defaultRotation.x, rotationY, 0));
    }
}
