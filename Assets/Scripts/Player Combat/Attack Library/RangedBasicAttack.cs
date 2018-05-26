using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class RangedBasicAttack : MonoBehaviour
{
    // Ability 1 projectile behavour. Apply damage stat to enemy hit.
    //TODO: make this physics based like previous work.
    
    // ProjectileReferences
    public int speed;
    public int force;
    public int damage;
    public float lifeSpan;

    Rigidbody2D rigi;
    SpriteRenderer rend;
    CapsuleCollider2D col;
    CircleCollider2D ignoreCol;
    public LayerMask enemy;
    

    void Awake()
    {
        #region Setting References
        col = GetComponent<CapsuleCollider2D>();
        rigi = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        ignoreCol = GameObject.Find("Player").GetComponent<CircleCollider2D>();
        #endregion
        rigi.AddRelativeForce(Vector2.right * force, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        Physics2D.IgnoreCollision(col, ignoreCol, ignore: true);        
    }

    private void Update()
    {
        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        lifeSpan -= Time.deltaTime;
        if(lifeSpan <= 0)
        {
            Destroy(gameObject, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        

        if(collision.gameObject.layer == 11)
        {
            Debug.Log("meme");

            if (collision.GetComponent<Health>())
            {
                Health test = collision.GetComponent<Health>();
                test.TakeDamage(damage);
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
