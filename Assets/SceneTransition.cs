using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private Scene scene;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        Debug.Log("Scene: " + scene.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (scene.name == "Level 1")
        {
            if (collision.CompareTag("Player"))
            {
                SceneManager.LoadScene("Level 2");
            }
        }
        else if (scene.name == "Level 2")
        {
            if (collision.CompareTag("Player"))
            {
                SceneManager.LoadScene("Finished");
            }
        }
    }
}
