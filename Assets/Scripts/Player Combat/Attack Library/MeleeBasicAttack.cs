using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBasicAttack : MonoBehaviour
{

    CursorMode cursor;
    RaycastCharacterController colInfo;
    Vector2 meleeDirection;
    public int damage;
    public float meleeReach;
    public LayerMask enemy;
    

    // Use this for initialization
    void Start()
    {
        colInfo = GetComponent<RaycastCharacterController>();
        cursor = GetComponent<CursorMode>();
        enemy = LayerMask.GetMask("Enemy");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        
    }

   public void Melee()
    {
        meleeDirection = new Vector2(colInfo.colInfo.faceDirection, 0);
        
        

        if (Input.GetMouseButton(0) && cursor.meleeMode)
        {
            Debug.DrawRay(transform.position, meleeDirection, Color.cyan);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, meleeDirection, meleeReach, LayerMask.NameToLayer("Enemy"));

            if (hit.collider.tag == ("Enemy")) // Fix
            {
                Debug.Log(hit.collider.name);
                Health test = hit.collider.GetComponent<Health>();
                test.TakeDamage(damage);
                
            }
            
        }
    }

   
}
