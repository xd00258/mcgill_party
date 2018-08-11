using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void PlayGame ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

	public void QuitGame () 
	{
		Debug.Log ("Quit");
		Application.Quit ();
	}

    public void PlayTutorial ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void Update()
    {
        if (Input.GetButtonDown("StartMenuTutorial"))
        {
            PlayTutorial();
        }
        if (Input.GetButtonDown("StartMenuMaster"))
        {
            // TODO: Enter game master menu
        }
        if (Input.GetButtonDown("StartMenuStart"))
        {
            PlayGame();
        }
        if (Input.GetButtonDown("StartMenuQuit"))
        {
            QuitGame();
        }
    }
}
