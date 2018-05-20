using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testenemyAI : MonoBehaviour
{
    public enum State {Patrol, Aggro, Attack, BattleMechanic1};
    State behaviourState;
    public float aggroRadius; // The radius of when the enemy can begin to aggro onto the player.

    // Use this for initialization
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
