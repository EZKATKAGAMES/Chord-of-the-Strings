using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureSwitch : MonoBehaviour
{
    public Transform Door;
    public bool DoorOpen = false;

    public float minimum = 2;
    public float maximum = 6;

    static float t = 0.0f;

    void Update()
    {
        if (DoorOpen == true)
        {
            Door.transform.position = new Vector3(-26.8f, Mathf.Lerp(minimum, maximum, t), 0);

            t += 0.5f * Time.deltaTime;

            if (t > 1.0f)
            {
                float temp = maximum;
                Door.transform.position = new Vector3(-26.8f, maximum, 0);
            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Star")
        {
            DoorOpen = true;
        }
    }
}
