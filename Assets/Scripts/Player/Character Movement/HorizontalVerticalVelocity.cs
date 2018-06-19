using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalVerticalVelocity : MonoBehaviour
{
    // The purpose of this script is to controll values which are used as a multiplier to control our velocity.
    // TODO: Make it better.

    public bool inputRecieved;
    public float horizontalVelocity;
    public float verticalVelocity;
    public float hVelocityPositiveLimit = 1; // By defualt we cannot bypass this multiplier for calculating our speed.
    public float vVelocityPositiveLimit = 1;
    public bool unCapVelocity;

    private void Update()
    {
        MoveRightLeft();
        MoveUpDown();
        VelocityCap();

        if (horizontalVelocity == 0 && verticalVelocity == 0)
        {
            inputRecieved = false;
        }
        else inputRecieved = true;
            

    }

    void VelocityCap()
    {
        if (unCapVelocity == false)
        {
            if (horizontalVelocity > hVelocityPositiveLimit) // Precausion to stop velocity going outside bounds.
            {
                horizontalVelocity = hVelocityPositiveLimit;
                return;
            }


            if (horizontalVelocity < 0 && Input.GetKeyDown(GameManager.GM.Right))
            {
                horizontalVelocity = 0;
            }

            if (horizontalVelocity > 0 && Input.GetKeyDown(GameManager.GM.Left))
            {
                horizontalVelocity = 0;
            }

            if (verticalVelocity > vVelocityPositiveLimit)
            {
                verticalVelocity = vVelocityPositiveLimit;
                return;
            }

            if (verticalVelocity < 0 && Input.GetKeyDown(GameManager.GM.Right))
            {
                verticalVelocity = 0;
            }

            if (verticalVelocity > 0 && Input.GetKeyDown(GameManager.GM.Left))
            {
                verticalVelocity = 0;
            }

        }
    }

    void MoveRightLeft()
    {
        #region Horizontal

        if (Input.GetKey(GameManager.GM.Right))
        {
            horizontalVelocity = 1f;
        }
        else if (Input.GetKey(GameManager.GM.Left))
        {
            horizontalVelocity = -1f;
        }
        else // Reset to zero when no input is recieved.
        {
            if (horizontalVelocity == 0)
                return; // dont run the code if we are stationary!
            horizontalVelocity = Mathf.Lerp(horizontalVelocity, 0, 1f);
        }
        #endregion
        // Clamp velocity to a reasonable limit.
        horizontalVelocity = Mathf.Clamp(horizontalVelocity, -1, 1);
    }

    void MoveUpDown()
    {
        #region Vertical
        if (Input.GetKey(GameManager.GM.Up))
        {
            verticalVelocity = 1f;
        }
        else if (Input.GetKey(GameManager.GM.Down))
        {
            verticalVelocity = -1f;
        }
        else // Reset to zero when no input is recieved.
        {
            if (verticalVelocity == 0)
                return; // dont run the code if we are stationary!
            verticalVelocity = Mathf.Lerp(verticalVelocity, 0, 1f);
        }

        #endregion

        verticalVelocity = Mathf.Clamp(verticalVelocity, -1, 1);
    }
}