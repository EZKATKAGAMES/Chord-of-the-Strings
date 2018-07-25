using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disipate : MonoBehaviour
{
    BoxCollider[] colliders;
    Renderer rend;
    Vector3 meme;// Temp storage
    public Vector3 def;//ault
    float xIntensity =0.5f;
    float yIntensity =0.01f;
    float zIntensity =1f;
    float shrinkThreshold = 6f;

    public bool shrinkPhase1; // Shrinking
    public bool shrinkPhase2; // Restoring


    //TODO: Object scale restoration
    // object auto scales once the threshold is reached
    // apply shader once is reaches the threshold

    void Start()
    {
        colliders = GetComponents<BoxCollider>();
        // Collider 1 = walkable
        // Collider 2 = detection
        // Store collider size
        meme = gameObject.transform.localScale;
        def = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // Set collider size
        gameObject.transform.localScale = meme;

        

        
    }

    private void OnTriggerStay(Collider other)
    {
        // If we hit the player
        if (other.GetComponent<PlayerCharacter>())
        {
            Shrink();
        }

    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerCharacter>())
        {
            shrinkPhase1 = true;
        }
    }

    void Shrink()
    {
        // If the object is in normal state, shrink
        if(shrinkPhase1 == true)
        {
            // gameobject is going negative size (large)
            meme.x -= xIntensity * Time.deltaTime;
            meme.y -= yIntensity * Time.deltaTime;
            meme.z -= zIntensity * Time.deltaTime;

            // must stop at threshold and then auto shrink the rest of the way
            if(meme.x >= def.x && meme.y >= def.y && meme.z >= def.z | meme.x <= 0 && meme.y <= 0 && meme.z <= 0)
            {
                shrinkPhase1 = false;
            }
        }
        
        // if the object gets too small, disable
        if (gameObject.transform.localScale.magnitude <= 3f)
        {
            gameObject.SetActive(false);
        }

    }

    void RestoreToDefault()
    {
        // If we arent at default, make it so.
        if (gameObject.transform.localScale.magnitude <= def.magnitude)
        {
            meme.x += xIntensity * Time.deltaTime;
            meme.y += yIntensity * Time.deltaTime;
            meme.z += zIntensity * Time.deltaTime;
        }

        
    }
    
    // Called from another script
    public IEnumerator Respawn()
    {
        Debug.Log("respawningObject");
        yield return new WaitForSeconds(3);
        gameObject.SetActive(true);
        
    }
}
