using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySeek : MonoBehaviour
{
    public GameObject target;
    public NavMeshAgent AI;

    void Start()
    {
        AI = gameObject.GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        AI.SetDestination(target.transform.position);
    }
}
