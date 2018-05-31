using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPortals : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        // Portal Back to Earth
        if (col.tag == "Earth")
        {
            SceneManager.LoadScene(0);

            Destroy(gameObject);
        }

        // Portal Back to HUB
        if (col.tag == "HUB")
        {
            SceneManager.LoadScene(1);

            Destroy(gameObject);
        }

        // Portal to Tutorial Combat - Puzzles
        if (col.tag == "TutorialCombat")
        {
            SceneManager.LoadScene(2);

            Destroy(gameObject);
        }

        // Portal to Tutorial Boss Fight
        if (col.tag == "TutorialBoss")
        {
            SceneManager.LoadScene(3);

            Destroy(gameObject);
        }
    }
}
