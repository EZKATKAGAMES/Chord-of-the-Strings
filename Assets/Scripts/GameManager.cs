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
    public KeyCode Ab1 { get; set; } 
    public KeyCode Ab2 { get; set; }
    public KeyCode Ab3 { get; set; }
    public KeyCode Ab4 { get; set; }


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
        Ab1 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("spell1", "Alpha1"));
        Ab2 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("spell2", "Alpha2"));
        Ab3 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("spell3", "Alpha3"));
        Ab4 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("spell4", "Alpha4"));



    }

}
