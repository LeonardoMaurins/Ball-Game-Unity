using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateAround : MonoBehaviour {

    public GameObject rotateAroundObject; // The platform the stars will rotate around

    public int speed; // The speed and direction at which they will

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(rotateAroundObject.transform.position, Vector3.up, speed * Time.deltaTime); // RotateAround similar to CameraController around the position of the platform
    }
}
