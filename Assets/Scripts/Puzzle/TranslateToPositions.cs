using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateToPositions : MonoBehaviour
{
    public Transform[] positions;
    public float waitTime;
    public bool start;
    public bool end;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == positions[0].position && transform.position != positions[1].position)
        {
            start = true;
            end = false;
        }
        if (transform.position == positions[1].position && transform.position != positions[0].position)
        {
            end = true;
            start = false;
        }
            
       

        StartCoroutine(Switch());
    }

    

    IEnumerator Switch()
    {
       // first move
        if (start)
        {
            yield return new WaitForSeconds(waitTime);
            transform.position = Vector3.MoveTowards(transform.position, positions[1].position, waitTime * Time.deltaTime);
            
        }
        // second move
        if (end)
        {
            yield return new WaitForSeconds(waitTime);
            transform.position = Vector3.MoveTowards(transform.position, positions[0].position, waitTime);
            
        }
            

        

        
    }
}
