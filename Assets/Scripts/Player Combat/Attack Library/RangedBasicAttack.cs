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


        if (rigi.velocity.magnitude <= destroyVel)
        {
            Destroy(gameObject, 0);
        }
    }


    void VelocityCap()
    {
        if(rigi.velocity.magnitude >= velocityLimit)
        {
            
        }
    }

    public void Move()
    {
        //transform.Translate(Vector2.right * Time.deltaTime * speed);
    }

    void ApplyDamage()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("REE");

        if(collision.gameObject.layer == enemy)
        {
           Health hp = collision.GetComponent<Health>();
           hp.TakeDamage(damage);
           
        }
    }
}
