using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability1 : MonoBehaviour
{
    //Ability1: While this spell is active a large bolt will begin to materialize from your harp.
    // The player will be rooted until the spell is fired. The longer the spell is charged for the more damage it will deal to a certain limit.

    SpellCasting reference;
    public float projectileSpeed;
    public float lifeSpan = 5f;
    float timer;
    // Use this for initialization
    void Awake()
    {
        GameObject meme = GameObject.Find("Player");
        reference = meme.GetComponent<SpellCasting>();
        projectileSpeed = reference.a1_Speed;
    }

    private void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {    
        transform.Translate(Vector2.right * Time.deltaTime * projectileSpeed);

        timer += Time.deltaTime;
        if (timer >= lifeSpan)
        {
            Destroy(gameObject, 0);
        }
    }

    private void FixedUpdate()
    {
        
    }



}
