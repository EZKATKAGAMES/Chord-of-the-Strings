using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script executes anything not done in the player controller script.

[RequireComponent (typeof (PlayerController))]
public class PlayerInput : MonoBehaviour
{

    PlayerController player;
    Vector2 directionalInput;
    HorzontalVerticalVelocity directionVel;

    // Use this for initialization
    void Start()
    {
        player = GetComponent<PlayerController>();
        directionVel = GetComponent<HorzontalVerticalVelocity>();
    }

    // Update is called once per frame
    void Update()
    {               
        directionalInput = new Vector2(directionVel.horizontalVelocity, directionVel.verticalVelocity);
        player.SetDirectionalInput(directionalInput);
        
        if (Input.GetKeyDown(GameManager.GM.Jump))
        {
            player.OnJumpInputDown();
        }

        if (Input.GetKeyUp(GameManager.GM.Jump))
        {
            player.OnJumpInputUp();
        }
    }

   
}
