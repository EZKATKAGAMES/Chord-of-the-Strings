using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    /// Actions which we can associate a key to.
    // Movement
    public KeyCode Jump { get; set; }
    public KeyCode Right { get; set; }
    public KeyCode Left { get; set; }
    public KeyCode Down { get; set; }
    public KeyCode Up { get; set; }
    // Combat
    public KeyCode primaryAttack { get; set; } // Mouse1
    public KeyCode specialAttack { get; set; } // Mouse2


    private void Awake()
    {
        if (GM == null)
        {
            DontDestroyOnLoad(GM);
            GM = this;
        }
        else if(GM != this)
        {
            Destroy(gameObject);
        }

        // Defaults
        Jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpkey", "Space"));
        Right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightkey", "D"));
        Left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftkey", "A"));
        Down = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("downkey", "S"));
        Up = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("upkey", "W"));

        

    }

}
