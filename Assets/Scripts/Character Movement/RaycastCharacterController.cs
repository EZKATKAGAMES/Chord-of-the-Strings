using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script handles the detection and calculations needed for movement using raycasts.
// Climbing is also handled in this script.

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class RaycastCharacterController : MonoBehaviour
{
    #region RayCast Variables

    const float skinWidth = .015f; // Rays casted using this offset so their origin is internal.
    const float distanceBetweenRays = 0.25f;
    public LayerMask collisionMask;
    public LayerMask ascendableMask;
    [HideInInspector]
    public int horizontalRayCount;
    [HideInInspector]
    public int verticalRayCount;

    float horizontalRaySpacing;
    float verticalRaySpacing;
    BoxCollider2D col;
    RaycastOrigins raycastOrigins;
    Vector2 castOrigin;
    Vector2 rayOrigin;
    RaycastHit2D hit;
    RaycastHit2D climbHit;
    RaycastHit2D fallHit;


    public CollisionInfo colInfo;
    string ascendDescend;
    [HideInInspector]
    Vector2 playerInput;

    #endregion

    public virtual void Start()
    {
        CalculateRaySpacing();
    }

    void Awake()
    {
        col = GetComponent<BoxCollider2D>();      
        colInfo.faceDirection = 1;
    }

    public void Move(Vector2 moveAmount, Vector2 input)
    {
        UpdateRaycastOrigins();
        colInfo.Reset();
        playerInput = input;

        if (moveAmount.x != 0)
        {
            colInfo.faceDirection = (int)Mathf.Sign(moveAmount.x); // positive value = right, negative value = left
            if(playerInput.x == 0)
            {
                colInfo.faceDirection = 0;
            }

            HorizontalCollisions(ref moveAmount);
        }

        if (moveAmount.y != 0)
        {
            VerticalCollisions(ref moveAmount);
        }

        transform.Translate(moveAmount); // move after moveAmount has been calculated
    }

    void HorizontalCollisions(ref Vector2 moveAmount)
    {
        float directionX = colInfo.faceDirection;
        float rayLength = Mathf.Abs(moveAmount.x) + skinWidth; // force a positive with abs, offset the negative skinwidth

        if (Mathf.Abs(moveAmount.x) < skinWidth)
        {
            rayLength = 2 * skinWidth;
        }

        #region Horizontal Casts
        for (int i = 0; i < horizontalRayCount; i++)
        {
            // IF we are in the down direction set origins to bottom left, otherwise set it to topleft
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i); // Cast rays from where we will be on the x axis
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);

            if (hit)
            {
                if (hit.collider.tag == "PlatformPhaseUP&DOWN") // Phase through bottom of platform (jump to top), down input to fall through top.
                {
                    if (directionX == 1 || hit.distance == 0)
                    {
                        continue;
                    }
                }

                moveAmount.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;
                
                // direction we are facing will be set based on collide direction.
                colInfo.left = directionX == -1;
                colInfo.right = directionX == 1;
            }

        }

        #endregion
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.blue;
       // Gizmos.DrawCube(transform.position, col.size * 1.1f);
    }

    void VerticalCollisions(ref Vector2 moveAmount)
    {
        float directionY = Mathf.Sign(moveAmount.y); // Set move direction to 1 or -1 depending on input keys pressed.
        float rayLength = Mathf.Abs(moveAmount.y) + skinWidth; // force a positive with abs, offset the negative skinwidth

        if (Mathf.Abs(moveAmount.y) < skinWidth)
        {
            rayLength = 2 * skinWidth;
        }

        #region Vertical Casts

        climbHit = Physics2D.BoxCast(transform.position, col.size, 0, Vector2.zero, 0, ascendableMask);
        fallHit = Physics2D.BoxCast(transform.position, col.size * 1.15f, 0, Vector2.zero, 0, collisionMask);

        PlayerController climbSpeed = GetComponent<PlayerController>(); // Ref

        
        for (int i = 0; i < verticalRayCount; i++)
        {

            // IF we are in the down direction set origins to bottom left, otherwise set it to topleft
            rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + moveAmount.x); // Cast rays from where we will be on the x axis
            hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);

            if (hit && colInfo.climb == false)
            {

                if (hit && colInfo.left)
                {
                    Debug.Log(hit.distance);
                }

                if (hit.collider.tag == "PlatformPhaseUP&DOWN") // Phase through bottom of platform (jump to top), down input to fall through top.
                    { // TODO: fix detection                   
                         if (directionY == 1 || hit.distance == 0) // Go through platforms bottom.
                         {
                           continue;
                         }
                         
                         if (playerInput.y == -1) // If input is down, dont run the rest of script: fall through.
                         {                               
                               colInfo.fallingThroughPlatform = hit.collider;

                               if (hit.collider == colInfo.fallingThroughPlatform)
                               {
                                 continue;
                               }
                        
                         }
                    }

                colInfo.fallingThroughPlatform = hit.collider;

               
                
                moveAmount.y = (hit.distance - skinWidth) * directionY;
                
                
                rayLength = hit.distance;

                colInfo.below = directionY == -1; // Set directions
                colInfo.above = directionY == 1;             
            }

        }

        #region Climbing

        if (!climbHit)
        {
            colInfo.climbCD = false;
        }

        if (colInfo.below)
        {
            colInfo.climbCD = false;
        }

        if (colInfo.climb) // If we are climbing and hit the 
        {
            if (fallHit && fallHit.transform.tag != "PlatformPhaseUP&DOWN")
            {
                colInfo.climb = false;
                return;
            }
        }
        
        if(colInfo.climb && playerInput.y == 0) // Stop gravity from acting while climbing with no y input.
        {
            moveAmount.y = 0;
        }

        if (climbHit)
        {

            if (Input.GetKeyDown(KeyCode.Space) && colInfo.climb == true)
            {
                StartCoroutine(CD()); // Short delay before allowing climbing again
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                colInfo.climb = false;                
                return;
            }

            // Make sure we cannot initiate climb when the floor below is not intended to be moved through.
            if (colInfo.below && !colInfo.climb && hit.distance < skinWidth && playerInput.y == -1)
            {
                return;
            }

            if (colInfo.climb && moveAmount.y != 0 && Input.GetKeyDown(KeyCode.Space))
            {
                colInfo.climb = false;
                return;
            }
            
            if (playerInput.y != 0) // UP or DOWN input while colliding with climbable object initiates climb.
            {
                if(colInfo.climbCD == false)
                {
                    colInfo.climb = true;
                }
            }

            if(playerInput.y != 0 && colInfo.climb)
            {
                if(colInfo.climbCD == false) // Allow movement when we havent jumped off vine recently.
                {
                    moveAmount.y = playerInput.y * Time.deltaTime * climbSpeed.climbSpeedMax;
                }
                else
                {
                    return;
                }
                
            }
            
        }
        else
        {
            colInfo.climb = false;
        }

        #endregion

        #endregion


    }

    IEnumerator CD()
    {
        colInfo.climbCD = true;
        yield return new WaitForSeconds(0.25f); // play with this value until cooldown seems fluid.
        colInfo.climbCD = false;
    }

    void UpdateRaycastOrigins()
    {
        Bounds bounds = col.bounds;
        bounds.Expand(skinWidth * -2);

        #region Setting raycasts positions along the colider bounds
        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);

        
        #endregion
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = col.bounds;
        bounds.Expand(skinWidth * -2);

        float boundsWidth = bounds.size.x;
        float boundsHeight = bounds.size.y;

        // Calculate ray count depending on the size of collider.
        horizontalRayCount = Mathf.RoundToInt(boundsHeight / distanceBetweenRays);
        verticalRayCount = Mathf.RoundToInt(boundsWidth / distanceBetweenRays);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    public struct CollisionInfo
    {
        public bool above, below, left, right, climb, climbCD;
        public Collider2D fallingThroughPlatform;
        public int faceDirection; // 1 = right/up | -1 = left/down

        public void Reset()
        {
            above = below = false;
            left = right = false;
        }        
    }

}