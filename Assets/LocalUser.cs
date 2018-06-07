using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Networking
{
    [RequireComponent(typeof(Player))]
    public class LocalUser : MonoBehaviour
    {
        private Player player;

        // Use this for initialization
        void Start()
        {
            // Lock cursor
            Cursor.lockState = CursorLockMode.Locked;
            player = GetComponent<Player>();
        }

        // Update is called once per frame
        void Update()
        {
            // If player presses escape, lock cursor inside the screen however making it visible
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.Confined;
            }

            // Allow the player to move and jump

            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");
            player.Move(h, v);

            if (Input.GetButtonDown("Jump"))
            {
                player.Jump();
            }
        }
    }
}