using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTranslation : MonoBehaviour
{
    public float a1Damage;
    public float translateSpeed;
    public float lifeSpan;
    Rigidbody rigi;
    void Start()
    {
        rigi = GetComponent<Rigidbody>();
        StartCoroutine(Death());
       //rigi.AddForce(Vector3.forward * translateSpeed, ForceMode.Impulse);
    }
    void Update()
    {
        // Unintended slanting!!! 
        gameObject.transform.Translate(Vector3.forward * (translateSpeed) * (Time.deltaTime));
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(lifeSpan);
        Destroy(gameObject);
    }
}
