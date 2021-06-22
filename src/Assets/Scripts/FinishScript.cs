using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishScript : MonoBehaviour {

    public static int sceneCount;

    public Text finishHint;

    void Start () {
        print("Finish" + MenuInterface.sceneCount);
    }
	
	void Update () {

	}

    void OnCollisionEnter(Collision collision) //Creating collision
    {
        if (collision.gameObject.CompareTag("Player")) // If the finish platform collides with the player tag
        {
            if (GameObject.FindGameObjectsWithTag("Diamond").Length == 0) // Finding out how many objects remain with the diamond tag in the level and making sure it's true if 0
            {
                if (MenuInterface.sceneCount == 2) // If the count from MenuInterface script is 2
                {
                    SceneManager.LoadScene("Congradulations"); // Loads Congradulations scene
                    MenuInterface.sceneCount = 0; // Resets count to 0
                }
                else
                {
                    SceneManager.LoadScene("Continue"); // Loading the next scene
                }
            }
            else // Displays a hint that the player still has more diamonds to collect
            {
                finishHint.gameObject.SetActive(true); // Sets the text to true aka visible
                Invoke("HideHint", 1.5f); // Invokes the method after 1.5 seconds
            }
        }
    }

    void HideHint() // Hides the text 
    {
        finishHint.gameObject.SetActive(false); // Sets the text to false aka hidden
    }
}
