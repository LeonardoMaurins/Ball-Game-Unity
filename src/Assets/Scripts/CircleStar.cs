using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleStar : MonoBehaviour { // Similar to cameraController script but without controller input

    public GameObject platform; //Creates variable for platform it'll rotate around

    private Vector3 star; //Holds star position

    public float angularSpeed; //*1 The variable of at which the speed the star will rotate around the platform

    void Start()
    {
        star = transform.position - platform.transform.position; // Gets the difference between the star and the platform's center
    }


    void LateUpdate() { //Runs every frame like update but is also guaranteed to run after all items have been processed in update

            transform.position = platform.transform.position + star; //Alligns star with position of the platform

        float starMovement = 1 * angularSpeed * Time.deltaTime;  //*1 Creating a float movement for moving the star's position with delta time
        if (!Mathf.Approximately(starMovement, 0f)) //*1 Checks if the starMovement is approximately around 0
        {
            transform.RotateAround(platform.transform.position, Vector3.up, starMovement); //*1 Rotates the star around the platform using transform.RotateAround
            star = transform.position - platform.transform.position; // Makes sure the camera stays away the same distance away from the ball as when it started
        }
    }
}
