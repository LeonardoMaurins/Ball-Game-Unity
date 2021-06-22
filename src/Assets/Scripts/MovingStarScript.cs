using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingStarScript : MonoBehaviour {

    public float direction;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction * Time.deltaTime, 0, 0);
	}
}
