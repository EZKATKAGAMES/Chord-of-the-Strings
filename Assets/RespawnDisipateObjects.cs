using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnDisipateObjects : MonoBehaviour
{
    public disipate[] objectsInScene;

    // Use this for initialization
    void Start()
    {
        
        GetComponents<disipate>();
        objectsInScene = FindObjectsOfType<disipate>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < objectsInScene.Length; i++)
        {
            if(objectsInScene[i].enabled == false)
            {
                // Respawn disabled objects
                objectsInScene[i].GetComponent<disipate>().Respawn();
            }
        }
    }
}
