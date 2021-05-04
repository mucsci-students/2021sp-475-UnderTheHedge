using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenuBack()
    {
        SceneManager.LoadScene(0);
    }

    public void Controls()
    {
        SceneManager.LoadScene(6);
    }
    
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Credits()
    {
        SceneManager.LoadScene(7);
    }

    public void Reload ()
    {
        SceneManager.LoadScene (mazeNo.mazeNumber);
    }
}

