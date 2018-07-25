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
        SetLerpValues();
        FollowTarget();
    }

  
    void FollowTarget()
    {
        // Target x, camerazoom Y, target Z
        // TODO: SMOOTHING
        gameObject.transform.position = new Vector3(target.transform.position.x, cameraZoom, target.transform.position.z);
    }

    void SetLerpValues()
    {
        float v1 = 45;
        float v2 = -45;
        float v3 = -140;
        float v4 = 140;

        if (rotationY == v1)
        {
            view2 = view3 = view4 = false;
            view1 = true;
            
        }
        else if (rotationY == v2)
        {
            view1 = view3 = view4 = false;
            view2 = true;

        }
        else if (rotationY == v3)
        {
            view2 = view1 = view4 = false;
            view3 = true;

        }
        else if (rotationY == v4)
        {
            view2 = view3 = view1 = false;
            view4 = true;

        }

        if (view1)
        {

            view2 = view3 = view4 = false;
            lerpTargetNegative = v2;
            lerpTargetPositive = v4;
        }

        if (view2)
        {
            view1 = view3 = view4 = false;
            lerpTargetNegative = v3;
            lerpTargetPositive = v1;
        }

        if (view3)
        {
            view1 = view2 = view4 = false;
            lerpTargetNegative = v4;
            lerpTargetPositive = v2;
        }

        if (view4)
        {
            view1 = view2 = view3 = false;
            lerpTargetNegative = v1;
            lerpTargetPositive = v3;
        }
    }

    void RotateCamera()
    {

        // Allow rotation when the camera is not currently rotating
        if (Input.GetKeyDown(KeyCode.E) && !cameraRotating)
        {
            cameraRotating = true;
            cameraRotateN = true;
            cameraRotateP = false;

        }

        if (cameraRotateN)
        {
            StartCoroutine(RotateProperly());
            //val = (1-t)*v0 + t* v1
            rotationY = (1 - rotationStrength) * rotationY + rotationStrength * lerpTargetNegative;
            
        }

     
        if (Input.GetKeyDown(KeyCode.Q) && !cameraRotating)
        {
            cameraRotating = true;
            cameraRotateP = true;
            cameraRotateN = false;
        }

        if (cameraRotateP)
        {
            StartCoroutine(RotateProperly());
            // val = (1-t)*v0 + t* v1
            //rotationY; FINISH
            
        }

        // Apply rotation
        // X is being set to static default value. Y is set to dynamic value we change.
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(defaultRotation.x, rotationY, 0));
       
        
    }

    
    public static float Lerp(float start, float finish, float percentage)
{
    //Make sure percentage is in the range [0.0, 1.0]
    percentage = Mathf.Clamp01(percentage);

    //(finish-start) is the Vector3 drawn between 'start' and 'finish'
    float startToFinish = finish - start;
 
    //Multiply it by percentage and set its origin to 'start'
    return start + startToFinish * percentage;
}

    IEnumerator RotateProperly()
    {
        yield return new WaitForSeconds(1.3f);
        cameraRotateN = false;
        cameraRotateP = false;
        cameraRotating = false;
    }
}
