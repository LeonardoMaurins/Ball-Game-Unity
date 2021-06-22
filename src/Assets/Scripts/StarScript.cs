using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
        transform.Rotate(0, -50f * Time.deltaTime, 0);

    }
}
