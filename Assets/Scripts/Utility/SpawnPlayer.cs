using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject player;
    public Transform spawnPoint;

    void Awake()
    {
        if (player != null)
            Instantiate(player,spawnPoint.position,this.player.transform.rotation);       
    }
}
