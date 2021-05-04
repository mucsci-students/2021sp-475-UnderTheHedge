using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchMaze : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SceneManager.LoadScene(5);
        }

    	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
