using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTranslation : MonoBehaviour
{
    public float translateSpeed;
    public float lifeSpan;
    void Start()
    {
        GameObject meme = GameObject.Find("ProjectileOrigin");
        transform.position = meme.transform.position;
        StartCoroutine(Death());
    }
    void Update()
    {
        gameObject.transform.Translate(Vector3.forward * (translateSpeed) * (Time.deltaTime));
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(lifeSpan);
        Destroy(gameObject);
    }
}
