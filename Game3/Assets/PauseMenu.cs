using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject pauseMenuUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
        	if (GamePaused)
        	{
        		Cursor.lockState = CursorLockMode.Locked;
        		Cursor.visible = false;
        		Resume();
        		
        	}
        	else
        	{
        		Cursor.lockState = CursorLockMode.None;
        		Cursor.visible = true;
        		Pause();
        	}
        }
    }

    public void Resume ()
    {
		pauseMenuUI.SetActive(false);
    	Time.timeScale = 1f;
    	GamePaused = false;
    }

    void Pause ()
    {
    	pauseMenuUI.SetActive(true);
    	Time.timeScale = 0f;
    	GamePaused = true;
    }

    public void LoadMenu ()
    {
    	Debug.Log("Loading menu...");
    	SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
    	Debug.Log("Quitting game...");
    	Application.Quit();
    }
}
