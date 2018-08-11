using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour{
    private Transform images;
    public int selectedImage = -1;

    public void Start()
    {
        images = GameObject.Find("Images").transform;
        PageSwitch();
    }

    public void Update()
    {
        if (Input.GetButtonDown("StartMenuTutorial"))
        {
            PageSwitch();
        }

        if (Input.GetButtonDown("StartMenuQuit"))
        {
            ReturnToMainMenu();
        }
    }

    public void PageSwitch()
    {
        selectedImage++;
        selectedImage %= images.childCount;
        int i = 0;
        foreach (Transform image in images)
        {
            if (i == selectedImage)
                image.gameObject.SetActive(true);
            else
                image.gameObject.SetActive(false);
            
            i++;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
