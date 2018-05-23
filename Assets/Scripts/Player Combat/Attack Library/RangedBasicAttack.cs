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
    public int lifeSpan;
    public int velocityLimit;
    public int destroyVel;

    Rigidbody2D rigi;
    SpriteRenderer rend;
    CapsuleCollider2D col;
    public LayerMask enemy;
    

    // Use this for initialization
    void Start()
    {
        #region Setting References
        col = GetComponent<CapsuleCollider2D>();
        rigi = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        #endregion

        rigi.AddRelativeForce(Vector2.right * force, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        InvokeRepeating("Move", 0, repeatRate: 0);
        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        lifeSpan -= (int)Time.deltaTime;
        if(lifeSpan <= 0)
        {
            Destroy(gameObject, 0);
        }

        if (rigi.velocity.magnitude <= destroyVel)
        {
            Destroy(gameObject, 0);
        }
    }


    void VelocityCap()
    {
       
    }

    public void Move()
    {
        //transform.Translate(Vector2.right * Time.deltaTime * speed);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {


        if(collision.gameObject.layer == 1 << 11)
        {
            print("enemyHit");

            if (collision.GetComponent<Health>())
            {
                collision.GetComponent<Health>().TakeDamage(damage);
                // Destroy
            }
           
        }
    }
}
