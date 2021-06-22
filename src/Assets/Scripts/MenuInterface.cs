using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInterface : MonoBehaviour {

    public static int sceneCount = 0; // SceneCount to determine how far into the game it is 

    //Scene currentScene; // Creates a currentScene variable

    // Use this for initialization
    void Start() {
        //currentScene = SceneManager.GetActiveScene(); // Sets currentScene to currently active scene
        print("Continue" + sceneCount);
    }

    // Update is called once per frame
    void Update() {
        //SpaceBar();

        /*if (Input.GetKey("return")) // Spacebar to run the ContinueScene function
        {
            ContinueScene();
            print("hi");
        }*/
    }

    /*public void playsound() // Plays sound if I connect to button but stops as soon as it switches scene
    {
        AudioSource menuSelect = GetComponent<AudioSource>();
        menuSelect.Play(); 
    }*/

    /*public void SpaceBar()
    {
        if (currentScene.name == "Continue") //If the current scenes name matches the continue string it runs it
        {
            if (Input.GetKey("return")) // Spacebar to run the ContinueScene function
            {
                ContinueScene();
                print("hi");
            }
        }
        if (currentScene.name == "Retry") //If the current scenes name matches the retry string it runs it
        {
            if (Input.GetKeyDown("space")) // Spacebar to run the RetryScene function
            {
                RetryScene();
            }
        }
    }*/

    /*void OnGUI()
    {
        if (Input.GetKeyDown("return"))
        {
            ContinueScene();
        }
    }*/

    private void ContinueScene() // Created a function that increases count after each time it runs the continue scene to progress through the game
    {
        if (sceneCount == 0){
            SceneManager.LoadScene("Level2");
            sceneCount++;
        }
        else if (sceneCount == 1)
        {
            SceneManager.LoadScene("Level3");
            sceneCount++;
        }
    }

    public void RetryScene() // Function that plays the same scene based on count
    {
        if (sceneCount == 0)
        {
            SceneManager.LoadScene("Level1");
        }
        if (sceneCount == 1)
        {
            SceneManager.LoadScene("Level2");
        }
        else if (sceneCount == 2)
        {
            SceneManager.LoadScene("Level3");
        }
    }

    public void MainMenu() // Goes to main menu and resets count
    {
        SceneManager.LoadScene("MainMenu");
        sceneCount = 0;
    }

    public void Level1() // Goes to Level 1 and resets count
    {
        SceneManager.LoadScene("Level1");
        sceneCount = 0;
    }

    public void Instructions() // Loads instructions scene
    {
        SceneManager.LoadScene("Instructions");
    }

    public void Exit() // Exits the game
    {
        Application.Quit(); //* Quit's the application 
    }
}
