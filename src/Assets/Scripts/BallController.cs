using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour {

    public bool cameraFollow; // Boolean for whether the camera is following the ball

    public Renderer ballDamage; // Creates public render variable for the ball

    private Rigidbody rb; // Creating variable of a rigidbody for a reference
    public float speed = 10; // Added a variable to modify speed of ball

    public Text diamondCount; // Creating public variable for the diamond count
    private int count; // Count of diamonds
    //public Text livesCount; // Creating public variable for the lives count 
    private int lives; // Count of lives
    public Text outOfBounds; // Creates variable for outOfBounds text

    public Text hint1; // Hints for the users which are set to disabled by default and turned on temporarily to display a hint before being set to inactive again
    public Text hint2;
    public Text hint3;

    private AudioSource collectSound; // Initialises audiosource of collectSound
    private AudioSource jumpSound; // Initialises audiosource of jumpSound
    private AudioSource ballSound; // Initialises audiosource of ballSound
    private AudioSource damageSound; // Initialises audiosource of damageSound
    private AudioSource explosionSound; // Initialises audiosource of explosionSound

    public RawImage heart1; // Used heart images to replace lives count for better game quality in the canvas
    public RawImage heart2;
    public RawImage heart3;

    float volume; //*3 Creates a variable

    void Start() // Code that runs from the start of the game
    {
        rb = GetComponent<Rigidbody>(); // Creating reference to any identified rigidbody

        AudioSource[] sources = GetComponents<AudioSource>(); // Created sources Array for AudioSource

        ballSound = sources[0]; // Obtaining ballSound AudioSource component from Array
        jumpSound = sources[1]; // Obtaining jumpSound AudioSource component from Array
        collectSound = sources[2]; // Obtaining collectSound AudioSource component from Array
        damageSound = sources[3]; // Obtaining damageSound AudioSource component from Array 
        explosionSound = sources[4]; // Obtaining explosionSound AudioSource component from Array 

        count = GameObject.FindGameObjectsWithTag("Diamond").Length; //*4 Setting count to the amount of objects it finds with the diamond tag
        SetDiamondCount(); // Executing the SetDiamondCount on start
        lives = 3; // Set's lives to default 3
        //SetLivesCount(); // Executes SetLivesCount on start

        cameraFollow = true; // Sets default boolean to true of cameraFollow

        ballDamage = GetComponent<Renderer>(); // Gets the renderer component for the variable

        Invoke("MineHint", 0); // Invokes the MineHint function on start of the level (In actual method makes sure its the third level)
    }

    void Update() //Update for jump because it runs it every frame rather than waiting for physics on other functions to finish before running
    {
        //MoveSound(); //Calling the MoveSound function every frame
        YouLose(); //Calling the YouLose function every frame
    }

    void FixedUpdate() { //Fixed update because it's called before performing any physics calculations
        if (cameraFollow) // Prevents control of ball if cameraFollow boolean is false
        {
            float moveHorizontal = Input.GetAxis("Horizontal"); //*1 Obtains horizontal input from the player and applies it to the initial variable
            float moveVertical = Input.GetAxis("Vertical"); //*1 Obtains vertical input from the player and applies it to the initial variable

            Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical); // Creating new Vector3 variable using input variables

            Vector3 fixedMovement = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * movement; //*2 Fix the rotation to match the direction of the camera's y axis.

            rb.AddForce(fixedMovement * speed); // Applying the force of the fixedMovement variable with the user's input for the direction of the ball
        }
    }

    /*void MoveSound() //*3 Function for the sound of the ball moving // Changed to onTriggerStay for ease of applying it to any ground tags its touching without needing to set parameters for y coordinates with if statements
    {

            volume = rb.velocity.magnitude * 1f; //*3 Sets the volume variable to the velocity of the rigidbody ball multiplied by 1f to get an accurate value
            ballSound.volume = volume / 20f; //*3 Sets the volume of the ballSound audioSource to that of the previous volume variable


            //ballSound.volume = 0; // Sets the ballSound volume to 0 if the ball is not within 0 and 0.5f

    }*/

    void OnTriggerEnter(Collider other) //On collision with other objects, runs script, using instead of OnCollisionEnter so ball doesn't collide with diamonds
    {
        if (other.gameObject.CompareTag("Diamond")) // If the other object has a Diamond tag
        {
            collectSound.Play(); //Plays the collect sound
            Destroy(other.gameObject); //Destroys the object it collided with
            count--; //Subtracts count
            SetDiamondCount(); //Executes the prior function
            ballDamage.material.color = Color.green; //Sets color of ball to green when it touches a Diamond
            Invoke("ReturnColor", 0.25f); // Invokes return to default color after 0.25 seconds
            Invoke("HintMessage", 0); // Invokes the HintMessage function for a tooltip when you collect the first diamond of the first level
        }
        else if (other.gameObject.CompareTag("Star")) // If the other object has a Star tag
        {
            damageSound.Play(); //Plays the damage sound
            Destroy(other.gameObject); //Destroys the object it collided with
            lives--; // Subtracts lives count
            //SetLivesCount(); // Exectures the prior function // No longer needed due to hearts
            ballDamage.material.color = Color.red; //Sets color of ball to red when it touches a Star
            Invoke("ReturnColor", 0.25f); // Invokes return to default color after 0.25 seconds
            Invoke("TakeHeart", 0);
        }
        else if (other.gameObject.CompareTag("Mine")) // If the other object has a Mine tag
        {
            explosionSound.Play(); //Plays the explosion sound
            Destroy(other.gameObject); //Destroys the object it collided with
            rb.AddForce(Random.Range(-1000f, 1000f), 500f, Random.Range(-1000f, 1000f)); //Adds force for colliding with mine as if an explosion
        }
    }

    private void OnCollisionExit(Collision collision) // Sets ballVolume to 0 so it doesn't make the rolling sound if its not on contact with any ground tags
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("finish"))
        {
            ballSound.volume = 0;
        }
    }

    private void OnCollisionStay(Collision collision) // OnCollisionStay so its running anytime the object is colliding with any compared tags
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("finish"))
        {
            if (Input.GetKey("space"))
            {
                Vector3 jump = new Vector3(0f, 200f, 0f); // Sets variable for jump 3D vector
                rb.AddForce(jump); // Adds jump variable to the rigidbody of the ball which makes it go upwards
                jumpSound.Play(); // Plays the jump sound
            }
            volume = rb.velocity.magnitude * 1f; //*3 Sets the volume variable to the velocity of the rigidbody ball multiplied by 1f to get an accurate value
            ballSound.volume = volume / 20f; //*3 Sets the volume of the ballSound audioSource to that of the previous volume variable
        }
    }

    /*private void OnCollisionEnter(Collision collision) // Didn't work because OnCollisionEnter only triggers when you're entering collision with the object, not a constant like the above
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (Input.GetKeyDown("space"))
            {
                Vector3 jump = new Vector3(0f, 400f, 0f); // Sets variable for jump 3D vector
                rb.AddForce(jump); // Adds jump variable to the rigidbody of the ball which makes it go upwards
                jumpSound.Play(); // Plays the jump sound
            }
        }
    }*/

    void ReturnColor() // ReturnColor function returns ball to default color
    {
        ballDamage.material.color = Color.white; // Sets ball to white returning default texture
    }

    void SetDiamondCount() // Function that sets the diamonds count
    {
        diamondCount.text = "Diamonds Remaining: " + count; // Updates diamonds count
    }

    /*void SetLivesCount() // Function that sets the lives count // No longer needed after adding hearts
    {
        livesCount.text = "Lives: " + lives; // Updates lives count
    }*/

    void YouLose() // Plays if you fall off the map or lose all your lives
    {
        if(rb.transform.position.y < -5) // Checks if the ball y position is below -5
        {
            cameraFollow = false; // Sets cameraFollow to false
            outOfBounds.gameObject.SetActive(true); // Sets outOfBounds text
        }

        if(rb.transform.position.y < -50 || lives == 0) // Checks if you fell off the map OR lost all your lives
        {
            SceneManager.LoadScene("Retry"); // Plays the Retry scene to restart the level
        }
    }

    void TakeHeart() // Method for reducing hearts onscreen
    {
        if(lives == 2) { // Takes first heart if lives are on 2
            heart1.gameObject.SetActive(false); // Sets raw image of heart to false hiding it
        }

        else if (lives == 1)
        {
            heart2.gameObject.SetActive(false);
        }

        else if (lives == 0)
        {
            heart3.gameObject.SetActive(false);
        }
    }

    void HintMessage() // Hint messages created with text, when called to checks how many diamonds are in the scene and if its the correct scene for intended purpose
    {
        if (GameObject.FindGameObjectsWithTag("Diamond").Length == 3 && MenuInterface.sceneCount == 0)
        {
            hint1.gameObject.SetActive(true);
            Invoke("HideHint", 1.5f); // Invokes the HideHint function to remove the hint off screen after 1.5 seconds
        }

        else if (GameObject.FindGameObjectsWithTag("Diamond").Length == 0 && MenuInterface.sceneCount == 1)
        {
            hint2.gameObject.SetActive(true);
            Invoke("HideHint", 1.5f);
        }

        else if (GameObject.FindGameObjectsWithTag("Diamond").Length == 4 && MenuInterface.sceneCount == 2)
        {
            hint3.gameObject.SetActive(true);
            Invoke("HideHint", 1.5f);
        }
    }

    void HideHint()
    {
            hint1.gameObject.SetActive(false); // Sets the hints gameObject back to false aka disabled
            hint2.gameObject.SetActive(false);
            hint3.gameObject.SetActive(false);
    }

    void MineHint() // Just like HintMessage but exclusive
    {
        if (GameObject.FindGameObjectsWithTag("Diamond").Length == 4 && MenuInterface.sceneCount == 2)
        {
            hint3.gameObject.SetActive(true);
            Invoke("HideHint", 2);
        }
    }
}

