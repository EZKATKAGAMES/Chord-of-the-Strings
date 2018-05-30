using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGM : MonoBehaviour
{
    public GameObject gameManager;
    void Awake()
    {
        if(GameManager.GM == null)
        {
            Instantiate(gameManager);
        }
    }
}
