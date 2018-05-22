using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
public class Health : MonoBehaviour
{
    public int health;
    void Update()
    {
        if(health < 0)
        {
            Destroy(gameObject, 0.1f);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        print("meme");
    }
}
