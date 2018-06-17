using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingSphere : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(col.gameObject);
        }
    }
}
