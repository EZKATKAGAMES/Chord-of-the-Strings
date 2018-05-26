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
    public float damage;
    float timer;
    // Use this for initialization
    void Awake()
    {
        GameObject meme = GameObject.Find("Player");
        reference = meme.GetComponent<SpellCasting>();
        projectileSpeed = reference.a1_Speed;
        damage = reference.a1_Damage;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {



        if (collision.gameObject.layer == 11)
        {
            Debug.Log("meme1");

            if (collision.GetComponent<Health>())
            {
                Health test = collision.GetComponent<Health>();
                test.TakeDamage((int)damage);
            }
        }

        // Maybe instantiate FX first?


        // Destroy on impact of these layers.
        if (collision.gameObject.layer == 1
            | collision.gameObject.layer == 2
            | collision.gameObject.layer == 3
            | collision.gameObject.layer == 4
            | collision.gameObject.layer == 5
            | collision.gameObject.layer == 8
            | collision.gameObject.layer == 10
            | collision.gameObject.layer == 11)
        {
            Destroy(gameObject, 0.05f);
        }



    }
}
