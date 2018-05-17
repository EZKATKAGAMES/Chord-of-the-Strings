using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMode : MonoBehaviour
{
    // This script will change cursor icons based on the distance the cursor is to the player.

    Texture2D meleeCursor;
    Texture2D rangedCursor;

    public float attackStyleSwitchRadius; // The size of area in which will determine wheter you perform melee or ranged attacks.
    public bool meleeMode;
    public bool rangeMode;

    public GameObject projectileOrigin;
    public Quaternion angle;

    #region CursorCheckVariables
    CircleCollider2D colRef;
    public Vector3 cursorPosition;
    Vector3 playerCenter; // Position in world space
    #endregion

    void Start()
    {
        Cursor.visible = true;

        // Set cursor icons.
        meleeCursor = Resources.Load("Icons/meleeicondraft") as Texture2D;
        rangedCursor = Resources.Load("Icons/rangedicondraft") as Texture2D;
        colRef = gameObject.GetComponent<CircleCollider2D>();

        projectileOrigin = GameObject.Find("ProjectileOrigin");

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(playerCenter, attackStyleSwitchRadius);
    }

    
    void Update()
    {
        attackStyleSwitchRadius = colRef.radius;
        playerCenter = colRef.transform.position;

        cursorPosition = Input.mousePosition;
        cursorPosition.z = Vector3.Distance(Camera.main.transform.position, playerCenter); // Correct the scale.

        float range = Vector3.Distance(Camera.main.ScreenToWorldPoint(cursorPosition),playerCenter);
        bool inside = range < attackStyleSwitchRadius;
               
        meleeMode = inside;
        rangeMode = !inside;

        if (meleeMode)
        {
            Cursor.SetCursor(meleeCursor, Vector2.zero, UnityEngine.CursorMode.Auto);
        }
        else if (rangeMode)
        {
            Cursor.SetCursor(rangedCursor, Vector2.zero, UnityEngine.CursorMode.Auto);       
        }



        

    }
    
}
