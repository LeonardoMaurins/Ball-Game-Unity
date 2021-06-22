using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player; //Creates variable for ball

    private new Vector3 camera; //Holds camera value

    public float angularSpeed; //*1 The variable of at which the speed the camera will rotate at

    private BallController ballController; // References the script of the ballController

    void Start () {
        camera = transform.position - player.transform.position; // Gets the difference between the camera and the player ball
        ballController = player.GetComponent<BallController>(); // Gets the reference of the ballController script for boolean
    }


    void LateUpdate () { //Runs every frame like update but is also guaranteed to run after all items have been processed in update
        if (ballController.cameraFollow)
        {
            transform.position = player.transform.position + camera; //Alligns camera with position of the player object
        }
        else
        {
            transform.LookAt(player.transform); // Makes the camera follow the player's location
        }
        
        float cameraMovement = Input.GetAxis("HorizontalArrows") * angularSpeed * Time.deltaTime;  //*1 Creating a variable for moving the camera's location using player input and changing at which speed
        if (!Mathf.Approximately(cameraMovement, 0f)) //*1 Checks if the cameramovement is approximately around 0
        {
            transform.RotateAround(player.transform.position, Vector3.up, cameraMovement); //*1 Rotates the camera around the ball using transform.RotateAround
            camera = transform.position - player.transform.position; // Makes sure the camera stays away the same distance away from the ball as when it started
        }
    }
}
