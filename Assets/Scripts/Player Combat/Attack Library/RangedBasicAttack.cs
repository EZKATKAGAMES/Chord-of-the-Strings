using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBasicAttack : MonoBehaviour
{
    // Ability 1 projectile behavour. Apply damage stat to enemy hit.
    //TODO: make this physics based like previous work.
    public int speed;
    public int damage;
    public int lifeSpan;
    CapsuleCollider2D col;
    public ContactFilter2D contact;
    Collider2D[] hitColliders;
    public LayerMask enemy;
    

    // Use this for initialization
    void Start()
    {
        col = GetComponent<CapsuleCollider2D>();
        enemy = 1 >> 11;
    }

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating("Move", 0, repeatRate: 0);
        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        InvokeRepeating("LifeSpan", 0, repeatRate: 1f);

        if(lifeSpan <= 0)
        {
            Destroy(gameObject, 0);
        }
    }

    void LifeSpan()
    {
        lifeSpan -= 1;
    }

    public void Move()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
    }

    void ApplyDamage()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if(collision.gameObject.layer == enemy)
        {
           Health hp = collision.otherCollider.GetComponent<Health>();
           hp.health -= damage;
           
        }
    }
}
